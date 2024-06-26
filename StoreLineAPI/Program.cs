using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistance.Context;
using Persistance.Repository;
using Persistance.Repository.Admin;
using Persistance.Repository.User;
using Application.DtoModels.Models.Admin;
using Application.MappingProfile.Admin;
using Application.MappingProfile.User;
using Application.Services;
using Application.Services.Implementations.Admin;
using Application.Services.Implementations.User;
using Application.Services.Interfaces;
using Application.Services.Interfaces.IRepository.Admin;
using Application.Services.Interfaces.IRepository.User;
using Application.Services.Interfaces.IServices.Admin;
using Application.Services.Interfaces.IServices.User;
using Application.UnitOfWork;
using CategoryAPI.Mapping;
using Microsoft.AspNetCore.Identity;
using Persistance;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<StoreLineContext>(options => options.UseNpgsql(connectionString));
        builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
            builder =>
            {
                builder.WithOrigins("http://www.sline.site")
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "StoreLine API", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "JWT Authorization header using the Bearer scheme",
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                    },
                    new List<string>()
                }
            });
        });

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
            };
        });

        builder.Services.AddAutoMapper(typeof(MappingAuthorization), typeof(MappingInformationAboutStoresStore), typeof(MappingCatalog),
            typeof(MappingBasket), typeof(MappingProfile), typeof(MappingCategory), typeof(MappingSubCategory),
            typeof(MappingDeliveryOptions), typeof(MappingProduct), typeof(MappingWarehouse), typeof(MappingStore));

        // Registering Scoped Services
        builder.Services.AddScoped(provider =>
        {
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var secretKey = jwtSettings["SecretKey"];
            var userManager = provider.GetRequiredService<UserManager<Users>>();
            return new JwtService(issuer, audience, secretKey, userManager);
        });
        builder.Services.AddScoped<UserManager<Users>>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<IInformationAboutStoreService, InformationAboutStoreService>();
        builder.Services.AddScoped<ICatalogService, CatalogService>();
        builder.Services.AddScoped<IBasketService, BasketService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IProfileService, ProfileService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
        builder.Services.AddScoped<IDeliveryOptionService, DeliveryOptionService>();
        builder.Services.AddScoped<IWarehouseService, WarehouseService>();
        builder.Services.AddScoped<IStoreService, StoreService>();
        builder.Services.AddScoped<IChainOfStoreService, ChainOfStoreService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddTransient<IEmailService, EmailService>();

        // Registering Scoped Repositories
        builder.Services.AddScoped<IInformationAboutStoresRepository, InformationAboutStoresRepository>();
        builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
        builder.Services.AddScoped<IBasketRepository, BasketReporitory>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
        builder.Services.AddScoped<IDeliveryOptionRepository, DeliveryOptionRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
        builder.Services.AddScoped<IStoreRepository, StoreRepository>();
        builder.Services.AddScoped<IChainOfStoreRepository, ChainOfStoreRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        // Identity Configuration
        builder.Services.AddIdentity<Users, IdentityRole>()
            .AddEntityFrameworkStores<StoreLineContext>()
            .AddRoles<IdentityRole>()
            .AddDefaultTokenProviders();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "StoreLine API V1");
            });
        }

        if (!app.Environment.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseCors("AllowAllOrigins");

        app.MapControllers();

        app.Run();
    }
}

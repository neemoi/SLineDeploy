using Application.DtoModels.Models.Admin;
using Application.DtoModels.Models.Authorization;
using Application.DtoModels.Response.Authorization;
using Application.Services.Interfaces.IServices.Admin;
using Application.Services.Interfaces.IServices.User;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Persistance;
using System.Web;


namespace Application.Services.Implementations.User
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtService _jwtService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public AccountService(UserManager<Users> userManager, SignInManager<Users> signInManager, 
            RoleManager<IdentityRole> roleManager, IMapper mapper, JwtService jwtService, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _jwtService = jwtService;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    throw new Exception($"Email {model.Email} not found");
                }

                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (!result.Succeeded)
                {
                    throw new Exception("Invalid password");
                }

                var token = await _jwtService.GenerateTokenAsync(user);
                var address = user.Address;

                var userRoles = await _userManager.GetRolesAsync(user);
                var role = userRoles.FirstOrDefault();

                var loginResponse = _mapper.Map<LoginResponseDto>(user);
                loginResponse.Token = token;
                loginResponse.Role = role;
                loginResponse.Address = address;

                return loginResponse;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in AccountService -> LoginAsync: {ex.Message}");
            }
        }

        public async Task<RegisterResponseDto> RegisterAsync(RegisterDto model)
        {
            try
            {
                var user = _mapper.Map<Users>(model);

                var emailAlreadyExists = await _userManager.FindByEmailAsync(model.Email);

                if (emailAlreadyExists != null)
                {
                    throw new Exception($"User with this {model.Email} email already exists");
                }

                if (!await _roleManager.RoleExistsAsync("User"))
                {
                    var role = new IdentityRole("User");

                    await _roleManager.CreateAsync(role);
                }

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded || user == null)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Error creating account: {errors}");
                }

                await _userManager.AddToRoleAsync(user, "User");

                await _signInManager.SignInAsync(user, true);
                
                var token = await _jwtService.GenerateTokenAsync(user);
                var address = user.Address;

                var registerResponse = _mapper.Map<RegisterResponseDto>(user);
                registerResponse.Token = token;
                registerResponse.Address = address;

                return registerResponse;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in AccountService -> RegisterAsync: {ex.Message}");
            }
        }

        public async Task<LogoutResponseDto> LogoutAsync(HttpContext httpContext)
        {
            try
            {
                var user = await _userManager.GetUserAsync(httpContext.User);

                await _signInManager.SignOutAsync();

                return _mapper.Map<LogoutResponseDto>(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in AccountService -> LogoutAsync: {ex.Message}");
            }
        }

        public async Task ForgotPasswordAsync(ForgotPasswordDto model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var resetLink = $"http://localhost:3000/reset-password?token={HttpUtility.UrlEncode(token)}&email={model.Email}";

                var message = $"Восстановление пароля: <a href='{resetLink}'>тут</a>.";

                await _emailService.SendEmailAsync(model.Email, "Восстановление пароля", message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in AccountService -> ForgotPasswordAsync: {ex.Message}");
            }
        }


        public async Task ResetPasswordAsync(ResetPasswordDto model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in AccountService -> ResetPasswordAsync: {ex.Message}");
            }
        }
    }
}

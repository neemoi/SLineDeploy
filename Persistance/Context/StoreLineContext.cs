using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Context;

public partial class StoreLineContext : IdentityDbContext<Users>
{
    public StoreLineContext() { }

    public StoreLineContext(DbContextOptions<StoreLineContext> options)
        : base(options) { }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ChainOfStore> ChainOfStores { get; set; }

    public virtual DbSet<DeliveryOption> DeliveryOptions { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<Subcategory> Subcategories { get; set; }

    public virtual DbSet<UserCart> UserCarts { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(e => new { e.UserId, e.LoginProvider });

        modelBuilder.Entity<IdentityUserRole<string>>().HasKey(e => new { e.UserId, e.RoleId });

        modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payment_pkey");

            entity.ToTable("payments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Commission).HasColumnName("commission");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(256);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);
            entity.Property(e => e.Birthday)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("birthday_date");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.CategoryImage).HasColumnName("category_image");
        });

        modelBuilder.Entity<ChainOfStore>(entity =>
        {
            entity.HasKey(e => e.ChainId).HasName("chain_of_stores_pkey");

            entity.ToTable("chain_of_stores");

            entity.Property(e => e.ChainId).HasColumnName("chain_id");
            entity.Property(e => e.ChainName)
                .HasMaxLength(255)
                .HasColumnName("chain_name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ChainImage).HasColumnName("chain_image");
        });

        modelBuilder.Entity<DeliveryOption>(entity =>
        {
            entity.HasKey(e => e.DeliveryId).HasName("delivery_options_pkey");

            entity.ToTable("delivery_options");

            entity.HasIndex(e => e.StoreId, "IX_delivery_options_store_id");

            entity.Property(e => e.DeliveryId).HasColumnName("delivery_id");
            entity.Property(e => e.DeliveryType).HasColumnName("delivery_type");
            entity.Property(e => e.DeliveryPrice)
                .HasPrecision(10, 2)
                .HasColumnName("delivery_price");
            entity.Property(e => e.DeliveryTime).HasColumnName("delivery_time");
            entity.Property(e => e.StoreId).HasColumnName("store_id");

            entity.HasOne(d => d.Store).WithMany(p => p.DeliveryOptions)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("delivery_options_store_id_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("orders_pkey");

            entity.Property(e => e.OrderId)
                .HasColumnName("order_id");

            entity.ToTable("orders");

            entity.HasIndex(e => e.CartId, "IX_orders_cart_id");

            entity.HasIndex(e => e.DeliveryId, "IX_orders_delivery_id");

            entity.HasIndex(e => e.StatusId, "IX_orders_status_id");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.DeliveryId).HasColumnName("delivery_id");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("order_date");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.Cart).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("orders_cart_id_fkey");

            entity.HasOne(d => d.Delivery).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DeliveryId)
                .HasConstraintName("orders_delivery_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("orders_status_id_fkey");

            entity.HasOne(d => d.Payment).WithMany(p => p.Orders)
               .HasForeignKey(d => d.PaymentId)
               .HasConstraintName("orders_payments_id_fkey");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("order_items_pkey");

            entity.ToTable("order_items");

            entity.HasIndex(e => e.OrderId, "IX_order_items_order_id");

            entity.HasIndex(e => e.ProductId, "IX_order_items_product_id");

            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductPrice)
                .HasPrecision(10, 2)
                .HasColumnName("product_price");
            entity.Property(e => e.TotalPrice)
                .HasPrecision(10, 2)
                .HasColumnName("total_price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Store)
                .HasMaxLength(255)
                .HasColumnName("store");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("order_items_order_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("order_items_product_id_fkey");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("order_statuses_pkey");

            entity.ToTable("order_statuses");

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("products_pkey");

            entity.ToTable("products");

            entity.HasIndex(e => e.SubcategoryId, "IX_products_subcategory_id");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Calories).HasColumnName("calories");
            entity.Property(e => e.Carbohydrates)
                .HasPrecision(6, 2)
                .HasColumnName("carbohydrates");
            entity.Property(e => e.Composition).HasColumnName("composition");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Fats)
                .HasPrecision(6, 2)
                .HasColumnName("fats");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(255)
                .HasColumnName("manufacturer");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .HasColumnName("product_name");
            entity.Property(e => e.Proteins)
                .HasPrecision(6, 2)
                .HasColumnName("proteins");
            entity.Property(e => e.ShelfLife).HasColumnName("shelf_life");
            entity.Property(e => e.StorageConditions).HasColumnName("storage_conditions");
            entity.Property(e => e.SubcategoryId).HasColumnName("subcategory_id");

            entity.HasOne(d => d.Subcategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.SubcategoryId)
                .HasConstraintName("products_subcategory_id_fkey");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("stores_pkey");

            entity.ToTable("stores");

            entity.HasIndex(e => e.ChainId, "IX_stores_chain_id");

            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.ChainId).HasColumnName("chain_id");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.StoreName)
                .HasMaxLength(255)
                .HasColumnName("store_name");

            entity.HasOne(d => d.Chain).WithMany(p => p.Stores)
                .HasForeignKey(d => d.ChainId)
                .HasConstraintName("stores_chain_id_fkey");
        });

        modelBuilder.Entity<Subcategory>(entity =>
        {
            entity.HasKey(e => e.SubcategoryId).HasName("subcategories_pkey");

            entity.ToTable("subcategories");

            entity.HasIndex(e => e.CategoryId, "IX_subcategories_category_id");

            entity.Property(e => e.SubcategoryId).HasColumnName("subcategory_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.SubcategoryName)
                .HasMaxLength(255)
                .HasColumnName("subcategory_name");
            entity.Property(e => e.SubcategoryImage).HasColumnName("subcategory_image");

            entity.HasOne(d => d.Category).WithMany(p => p.Subcategories)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("subcategories_category_id_fkey");
        });

        modelBuilder.Entity<UserCart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("user_cart_pkey");

            entity.ToTable("user_cart");

            entity.HasIndex(e => e.ProductId, "IX_user_cart_product_id");

            entity.HasIndex(e => e.StoreId, "IX_user_cart_store_id");

            entity.HasIndex(e => e.UserId, "IX_user_cart_user_id");

            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Price).HasColumnName("price"); 
            entity.Property(e => e.IsOrdered).HasColumnName("is_ordered");

            entity.HasOne(d => d.Product).WithMany(p => p.UserCarts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("usercart_product_id_id_fkey");

            entity.HasOne(d => d.Store).WithMany(p => p.UserCarts)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("usercart_store_id_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserCarts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("usercart_user_id_id_fkey");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.WarehouseId).HasName("warehouse_pkey");

            entity.ToTable("warehouse");

            entity.HasIndex(e => e.ProductId, "IX_warehouse_product_id");

            entity.HasIndex(e => e.StoreId, "IX_warehouse_store_id");

            entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductPrice)
                .HasPrecision(10, 2)
                .HasColumnName("product_price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.StoreId).HasColumnName("store_id");

            entity.HasOne(d => d.Product).WithMany(p => p.Warehouses)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("warehouse_product_id_fkey");

            entity.HasOne(d => d.Store).WithMany(p => p.Warehouses)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("warehouse_store_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

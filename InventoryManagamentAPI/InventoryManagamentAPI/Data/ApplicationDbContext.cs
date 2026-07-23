using InventoryManagamentAPI.Models;
using InventoryManagamentAPI.Models.Stripe;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore; 
using Product = InventoryManagamentAPI.Models.Stripe.Product;

namespace InventoryManagamentAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<InventoryProcessDetail> InventoryProcessDetails { get; set; }
        public DbSet<InventoryProcess> InventoryProcesses { get; set; }

        #region Stripe
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        //public DbSet<UserInventory> UserInventories { get; set; }
        public DbSet<ItemInventory> ItemInventories { get; set; }

        #endregion
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Item>().HasData(
                new Item
                {
                    Id = 1,
                    Name="Caramel A",
                    Price=21.32m,
                    Quantity=21
                },
                new Item
                {
                    Id = 2,
                    Name="Caramel B",
                    Price=7.1m,
                    Quantity=3
                },
                new Item
                {
                    Id = 3,
                    Name="Caramel C",
                    Price=54.542m,
                    Quantity=2176
                }
                );
            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name="Caramel Combo Pack",
                    Price=25.99m,
                    YieldContent="CaramelComboPack"
                }
                );
        }

    }
}

using InventoryManagamentAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagamentAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<InventoryProcessDetail> InventoryProcessDetails { get; set; }
        public DbSet<InventoryProcess> InventoryProcesses { get; set; }
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
        }
    }
}

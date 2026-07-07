using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPIBeginnerHerryWijaya.Models.Project1FinanceTracker;

namespace WebAPIBeginnerHerryWijaya.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Finance> Finances { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }
    }
}

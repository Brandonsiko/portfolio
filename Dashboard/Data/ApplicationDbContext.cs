using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Dashboard.Models;

namespace Dashboard.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Dashboard.Models.InventoryItems>? InventoryItems { get; set; }
        public DbSet<Dashboard.Models.MainStore>? MainStore { get; set; }
        public DbSet<Dashboard.Models.Operation>? Operation { get; set; }

    }
}
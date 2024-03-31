using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alumnium.Core.DbContext
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext() { }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) 
        {
        }
        public DbSet<AlumniumType> AlumniumTypes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public DbSet<SiteRequest> SiteRequests { get; set; }
        public DbSet<SiteUsedAlumimum> SiteUsedAlumimums { get; set; }
        public DbSet<AlmuniumUsedItems> AluminumUsedItems { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=AlumniumWorkshop;Trusted_Connection=True;MultipleActiveResultSets=true");

            //optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=AlumniumWorkshop;Trusted_Connection=True;MultipleActiveResultSets=true");

            base.OnConfiguring(optionsBuilder);
        }
    }
}

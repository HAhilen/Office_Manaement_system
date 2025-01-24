using Microsoft.EntityFrameworkCore;
using WebApplication2.Entities;

namespace WebApplication2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<Employee>()
                .HasOne<Organization>()  
                .WithMany() 
                .HasForeignKey(e => e.OrganizationId) 
                .OnDelete(DeleteBehavior.SetNull); 
            modelBuilder.Entity<Organization>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Employee>()
                .HasKey(e => e.Id);
        }
    }
}
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
                .WithMany(x=>x.Employees) 
                .HasForeignKey(e => e.OrganizationId) 
                .OnDelete(DeleteBehavior.SetNull); 
            modelBuilder.Entity<Organization>()
                .HasKey(x=>x.Id)  ;
            modelBuilder.Entity<Organization>().HasMany(x=>x.Employees).WithOne(x=>x.Organization).HasForeignKey(x=>x.OrganizationId);

            modelBuilder.Entity<Employee>()
                .HasKey(e => e.Id);
        }
    }
}
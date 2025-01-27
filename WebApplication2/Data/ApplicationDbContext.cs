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
        public DbSet<Department> Departments { get; set; }
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

            // Configure Employee-Organization relationship (Many-to-One)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Organization)
                .WithMany(o => o.Employees)
                .HasForeignKey(e => e.OrganizationId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Organization>()
                .HasKey(o => o.Id); 
            modelBuilder.Entity<Organization>()
                .HasMany(x=>x.Employees)
                .WithOne(x=>x.Organization)
                .HasForeignKey(x=>x.OrganizationId);

            // Configure Employee-Department relationship (Many-to-One)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull); // Set null on delete, if desired

            modelBuilder.Entity<Department>()
                .HasKey(d => d.Id); // Primary key for Department
            
            

            // Configure reverse relationship (Department has many Employees)
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId);
        }
    }
}
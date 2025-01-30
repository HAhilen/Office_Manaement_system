using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication2.Configurations
{
    public class DepartmentConfig : IEntityTypeConfiguration<Entities.Department>
    {
        public void Configure(EntityTypeBuilder<Entities.Department> builder)
        { 
            builder.ToTable("department", "public");
            //one-to-many relationship between Department and Employee
            builder.HasKey(d => d.Id);
            builder.HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
            
            // One-to-one relationship between Department and Manager
            builder.HasOne(d => d.Manager)    
                .WithMany(m => m.Departments) 
                .HasForeignKey(d => d.ManagerId)  
                .OnDelete(DeleteBehavior.SetNull);
        }
      
    }
}

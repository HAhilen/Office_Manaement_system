using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication2.Configurations
{
    public class ManagerConfig : IEntityTypeConfiguration<Entities.Manager>
    {
       
            public void Configure(EntityTypeBuilder<Entities.Manager> builder)
            {
                builder.ToTable("manager", "hr_management");

                builder.HasKey(m => m.Id);

                // One-to-many relationship between Manager and Employees
                builder.HasMany(m => m.Employees)
                    .WithOne(e => e.Manager)
                    .HasForeignKey(e => e.ManagerId);

                // Reverse relationship - Manager has one Department
                builder.HasOne(m => m.Department)
                    .WithMany(d => d.Managers)
                    .HasForeignKey(m => m.DepartmentId);
            }
        


    }
}
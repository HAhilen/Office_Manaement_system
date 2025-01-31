using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication2.Configurations
{
    public class DepartmentConfig : IEntityTypeConfiguration<Entities.Department>
    {
       
            public void Configure(EntityTypeBuilder<Entities.Department> builder)
            {
                builder.ToTable("department", "hr_management");

                builder.HasKey(d => d.Id);

                // One-to-many relationship between Department and Manager
                builder.HasMany(d => d.Managers)
                    .WithOne(m => m.Department)
                    .HasForeignKey(m => m.DepartmentId);

                // Reverse relationship - Department has many Employees
                builder.HasMany(d => d.Employees)
                    .WithOne(e => e.Department)
                    .HasForeignKey(e => e.DepartmentId);

            builder.HasOne(x => x.Organization).WithMany(x => x.Departments).HasForeignKey(x => x.OrganizationId);
            }
        



    }
}

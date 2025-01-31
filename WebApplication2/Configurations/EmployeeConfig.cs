using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication2.Entities;

namespace WebApplication2.Configurations
{
   
        public class EmployeeConfig : IEntityTypeConfiguration<Employee>
        {
            public void Configure(EntityTypeBuilder<Employee> builder)
            {
                builder.ToTable("employee", "hr_management");

                builder.HasKey(e => e.Id);

               
                // Many-to-one relationship between Employee and Department
                builder.HasOne(e => e.Department)
                    .WithMany(d => d.Employees)
                    .HasForeignKey(e => e.DepartmentId);

                // Many-to-one relationship between Employee and Manager
                builder.HasOne(e => e.Manager)
                    .WithMany(m => m.Employees)
                    .HasForeignKey(e => e.ManagerId);
            }
        }

}


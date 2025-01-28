using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication2.Entities;

namespace WebApplication2.Configurations
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("employee", "public");
           

            builder.HasOne(x => x.Organization).WithMany(x => x.Employees).HasForeignKey(x => x.OrganizationId).OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Department).WithMany(x => x.Employees).HasForeignKey(x => x.DepartmentId)
               .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

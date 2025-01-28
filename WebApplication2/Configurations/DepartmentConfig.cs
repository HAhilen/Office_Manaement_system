using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication2.Configurations
{
    public class DepartmentConfig : IEntityTypeConfiguration<Entities.Department>
    {
        public void Configure(EntityTypeBuilder<Entities.Department> builder)
        {
            builder.ToTable("department", "public");
            builder.HasKey(d => d.Id);
            builder.HasMany(d => d.Employees).WithOne(e => e.Department).HasForeignKey(e => e.DepartmentId);

        }
    }
}

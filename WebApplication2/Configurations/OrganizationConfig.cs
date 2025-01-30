using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Entities;
using System.Reflection.Emit;

namespace WebApplication2.Configurations
{
    public class OrganizationConfig : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            // // One-to-many relationship between Organization and Employee
            
            builder.ToTable("organization", "public");           
            builder.HasKey(o => o.Id);
            builder.HasMany(x => x.Employees)
                .WithOne(x => x.Organization)
                .HasForeignKey(x => x.OrganizationId);
        }
    }
}

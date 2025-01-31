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
            builder.ToTable("organization", "hr_management");

            builder.HasKey(o => o.Id);

            // One-to-many relationship between Organization and Department
            builder.HasMany(o => o.Departments)
                .WithOne(d => d.Organization)
                .HasForeignKey(d => d.OrganizationId);


            
         
        }
    }


}

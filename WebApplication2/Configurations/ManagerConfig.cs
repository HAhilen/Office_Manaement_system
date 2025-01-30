using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication2.Configurations
{
    public class ManagerConfig : IEntityTypeConfiguration<Entities.Manager>
    {
        public void Configure(EntityTypeBuilder<Entities.Manager> builder)
        {
            //// One-to-many relationship between Manager and Department
            builder.ToTable("manager", "public");
            builder.HasKey(m => m.Id);
            builder.HasMany(m => m.Departments) 
                .WithOne(d => d.Manager) 
                .HasForeignKey(d => d.ManagerId) 
                .OnDelete(DeleteBehavior.SetNull);
            
            // Configure the one-to-many relationship between Manager and Organization
            builder.HasOne(m => m.Organization) 
                .WithMany(o => o.Managers)     
                .HasForeignKey(m => m.OrganizationId) 
                .OnDelete(DeleteBehavior.SetNull);
           
        }
    }
}
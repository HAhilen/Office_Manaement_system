using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Npgsql;
using System.Reflection;


namespace WebApplication2.Data
{
    public class ApplicationDbContext : DbContext
    {

        private readonly string _connectionString;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = configuration.GetSection("DbConfig:Host").Value,
                Port = configuration.GetSection("DbConfig:Port").Value?.ToInt() ?? 5432,
                Database = configuration.GetSection("DbConfig:Database").Value,
                Username = configuration.GetSection("DbConfig:UserName").Value ?? "postgres",
                Password = configuration.GetSection("DbConfig:Password").Value ?? "123456789",
                IncludeErrorDetail = true,
                ApplicationName = "OfficeManagementSystem"
            };

            _connectionString = builder.ConnectionString;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseNpgsql(_connectionString)
                    .UseSnakeCaseNamingConvention()
                    .UseLazyLoadingProxies()
                    .ConfigureWarnings(w => w.Ignore(CoreEventId.DetachedLazyLoadingWarning));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyAllConfigurations(Assembly.GetExecutingAssembly());
        }
    }


}
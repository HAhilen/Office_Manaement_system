using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace WebApplication2
{
    public static class ModelBuilderExtension
    {
        public static void ApplyAllConfigurations(this ModelBuilder modelBuilder, Assembly assembly)
        {
            var applyGenericMethod = typeof(ModelBuilder)
                .GetMethods()
                .First(m => m.Name == nameof(ModelBuilder.ApplyConfiguration)
                            && m.GetParameters().FirstOrDefault()?.ParameterType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));

            var types = assembly.GetTypes()
                .Where(c => c.IsClass && !c.IsAbstract && c.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

            foreach (var type in types)
            {
                var entityType = type.GetInterfaces().First().GenericTypeArguments.First();
                var applyConcreteMethod = applyGenericMethod.MakeGenericMethod(entityType);
                var configurationInstance = Activator.CreateInstance(type);
                applyConcreteMethod.Invoke(modelBuilder, new[] { configurationInstance });
            }
        }
    }
}

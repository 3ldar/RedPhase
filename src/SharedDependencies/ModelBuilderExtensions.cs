using System.Reflection;

using Microsoft.EntityFrameworkCore;

using RedPhase.Entities.Base;

namespace RedPhase.SharedDependencies
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// <see cref="DbContext"/> üzerinde <see cref="EntityBase"/> tipinden türeyen entitylere default Hilo larını atar.
        /// </summary>
        /// <param name="context"><see cref="DbContext"/></param>
        /// <param name="modelBuilder"><see cref="ModelBuilder"/></param>
        /// <param name="startValue">Başlangıç Değeri Default : <c>1000</c></param>
        /// <param name="incrementBy">Adım büyüklüğü Default : <c>1</c></param>
        /// <returns></returns>
        public static ModelBuilder AddHilos(this DbContext context, ModelBuilder modelBuilder, int startValue = 1000, int incrementBy = 100)
        {
            var baseType = typeof(EntityBase);

            var targetProperties = GetDbSetProperties(context, baseType);
            foreach (var property in targetProperties)
            {
                var propertyType = property.PropertyType.GenericTypeArguments[0];
                var hiloName = $"{propertyType.Name}Id_HiLo";
                modelBuilder.Entity(propertyType).Property(nameof(EntityBase.Id)).UseHiLo(hiloName);
                modelBuilder.HasSequence(hiloName).StartsAt(startValue).IncrementsBy(incrementBy);

            }
            return modelBuilder;
        }

        private static IEnumerable<PropertyInfo> GetDbSetProperties(DbContext context, Type baseType = null)
        {
            var type = context.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var targetProperties = properties.Where(r => r.PropertyType.GenericTypeArguments.Length == 1);
            if (baseType != null)
            {
                targetProperties = targetProperties.Where(r => r.PropertyType.GenericTypeArguments[0].IsSubclassOf(baseType));
            }

            return targetProperties;
        }
    }
}

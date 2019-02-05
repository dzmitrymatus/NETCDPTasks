using System;
using System.Linq;
using System.Reflection;

namespace MyDIContainer.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsTypeHasPropertiesWithAttribute<T>(this Type type)
        {
            return type.GetProperties().Any(property => property.GetCustomAttribute(typeof(T)) != null);
        }

        public static bool IsTypeHasAttribute<T>(this Type type)
        {
            return Attribute.IsDefined(type, typeof(T));
        }
    }
}

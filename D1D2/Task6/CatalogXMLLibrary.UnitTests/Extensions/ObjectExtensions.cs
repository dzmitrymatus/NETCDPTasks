using System.Linq;

namespace CatalogXMLLibrary.UnitTests.Extensions
{
    public static class ObjectExtensions
    {
        public static bool PropertiesEquals<T>(T firstEntity, T secondEntity)
        {
            var libraryEntityProperties = firstEntity.GetType().GetProperties();

            for (int i = 0; i < libraryEntityProperties.Count(); i++)
            {
                var firstObjectProperty = libraryEntityProperties[i].GetValue(firstEntity);
                var secondObjectProperty = libraryEntityProperties[i].GetValue(secondEntity);
                if (firstObjectProperty.Equals(secondObjectProperty) == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

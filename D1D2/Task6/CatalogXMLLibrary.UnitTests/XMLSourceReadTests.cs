using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using CatalogXMLLibrary.UnitTests.Extensions;
using CatalogXMLLibrary.UnitTests.TestData;
using CatalogXMLLibrary.XMLLibrarySource;

namespace CatalogXMLLibrary.UnitTests
{
    [TestFixture]
    public class XMLSourceReadTests
    {
        private XmlSource _source;

        [OneTimeSetUp]
        public void Initialize()
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(XmlDataSource.RawXmlData));
            _source = new XmlSource(stream);
        }

        [Test]
        public void ReadAllItemsCountTest()
        {            
            var items = _source.Read();
            Assert.That(items.Count() == XmlDataSource.XmlEntities.Count());
        }

        [Test]
        public void ReadAllItemsDataTest()
        {
            var actualItems = _source.Read();
            var expectedItems = XmlDataSource.XmlEntities.GetEnumerator();
            expectedItems.MoveNext();

            foreach (var actualItem in actualItems)
            {
                var expectedItem = expectedItems.Current;               
                Assert.That(ObjectExtensions.PropertiesEquals(actualItem, expectedItem),
                    $"'{actualItem.ToString()}' does not equal to '{expectedItem.ToString()}'!");
                expectedItems.MoveNext();
            }
        }      
    }
}

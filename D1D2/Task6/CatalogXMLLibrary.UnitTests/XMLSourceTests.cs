using CatalogXMLLibrary.UnitTests.TestData;
using CatalogXMLLibrary.XMLLibrarySource;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Text;

namespace CatalogXMLLibrary.UnitTests
{
    [TestFixture]
    public class XMLSourceTests
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
    }
}

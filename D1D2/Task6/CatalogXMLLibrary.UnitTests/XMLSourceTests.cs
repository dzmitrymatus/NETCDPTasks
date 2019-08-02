using CatalogXMLLibrary.XMLLibrarySource;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace CatalogXMLLibrary.UnitTests
{
    [TestFixture]
    public class XMLSourceTests
    {
        private XmlSource _source;

        [OneTimeSetUp]
        public void Initialize()
        {
            var stream = new FileStream("XMLLibrary.xml", FileMode.Open, FileAccess.ReadWrite);
            _source = new XmlSource(stream);
        }

        [Test]
        public void ReadAllItemsTest()
        {            
            var items = _source.Read();
            Assert.That(items.Count() == 3);
        }
    }
}

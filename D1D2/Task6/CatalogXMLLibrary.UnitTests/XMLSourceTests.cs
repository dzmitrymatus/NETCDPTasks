using CatalogXMLLibrary.XMLLibrarySource;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace CatalogXMLLibrary.UnitTests
{
    [TestFixture]
    public class XMLSourceTests
    {
        [Test]
        public void ReadAllItemsTest()
        {
            var stream = new FileStream("XMLLibrary.xml", FileMode.Open, FileAccess.ReadWrite);
            var source = new XmlSource(stream);
            var items = source.Read();
            Assert.That(items.Count() == 3);
        }
    }
}

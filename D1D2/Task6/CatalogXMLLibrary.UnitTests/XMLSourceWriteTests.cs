using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using CatalogXMLLibrary.UnitTests.TestData;
using CatalogXMLLibrary.XMLLibrarySource;

namespace CatalogXMLLibrary.UnitTests
{
    [TestFixture]
    public class XMLSourceWriteTests
    {
        private XmlSource _source;
        private MemoryStream _stream;

        [OneTimeSetUp]
        public void Initialize()
        {
            _stream = new MemoryStream();
            _source = new XmlSource(_stream);
        }

        [Test]
        public void WriteAllItemsDataTest()
        {
            _source.Write(XmlDataSource.XmlEntities);
            var actual = _stream.ToString();

            Assert.That(actual == XmlDataSource.RawXmlData);
        }
    }
}

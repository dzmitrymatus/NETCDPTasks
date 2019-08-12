using System.IO;
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
            _stream.Position = 0;
            TextReader reader = new StreamReader(_stream);
            var actual = reader.ReadToEnd();
       
            Assert.AreEqual(XmlDataSource.RawXmlData, actual);
        }
    }
}

using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using CatalogXMLLibrary.Domain.Models;
using CatalogXMLLibrary.XMLLibrarySource.Interfaces;
using CatalogXMLLibrary.XMLLibrarySource.Concrete.XmlElementParsers;

namespace CatalogXMLLibrary.XMLLibrarySource.Concrete
{
    public class XmlSourceReader : IXmlSourceReader
    {
        #region Constants
        private const string _libraryTagName = "library";
        #endregion

        #region Fields
        private readonly XmlReaderSettings _settings = new XmlReaderSettings()
        {
            IgnoreComments = true,
            IgnoreWhitespace = true
        };
        #endregion

        #region Constructors
        public XmlSourceReader()
        {
            Parsers = new List<IXmlElementParser>()
            {
                new BookElementParser(),
                new NewspaperElementParser(),
                new PatentElementParser()
            };
        }

        public XmlSourceReader(ICollection<IXmlElementParser> parsers)
        {
            Parsers = parsers;
        }
        #endregion

        #region Properties
        public ICollection<IXmlElementParser> Parsers { get; set; }
        #endregion

        #region Public methods
        public IEnumerable<LibraryEntity> ReadEntities(Stream source)
        {
            using (var xmlReader = XmlReader.Create(source, _settings))
            {
                xmlReader.ReadToFollowing(_libraryTagName);
                xmlReader.ReadStartElement(_libraryTagName);

                while (xmlReader.EOF == false && xmlReader.Name != _libraryTagName)
                {
                    if(xmlReader.NodeType == XmlNodeType.Element)
                    {
                        yield return ParseEntity(xmlReader);
                    }
                }
            }
        }
        #endregion

        #region Private methods
        private LibraryEntity ParseEntity(XmlReader xmlReader)
        {
            var node = XNode.ReadFrom(xmlReader) as XElement;
            var parser = Parsers.FirstOrDefault(x => x.ElementTag == node.Name);
            if (parser == null) throw new Exception($"Unexpected element: '{node.Name}'. Reader doesn't have parser for this element!");
            return parser.Parse(node);
        }
        #endregion
    }
}

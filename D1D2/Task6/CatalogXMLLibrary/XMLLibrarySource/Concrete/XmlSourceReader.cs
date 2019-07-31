using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using CatalogXMLLibrary.Domain.Models;
using CatalogXMLLibrary.XMLLibrarySource.Interfaces;

namespace CatalogXMLLibrary.XMLLibrarySource.Concrete
{
    public class XmlSourceReader : IXmlSourceReader
    {
        #region Constants
        private const string _libraryTagName = "library";
        #endregion

        #region Fields
        private IEnumerable<IXmlElementParser> _parsers;
        private XmlReaderSettings _settings;
        #endregion

        #region Constructors
        public XmlSourceReader(IEnumerable<IXmlElementParser> parsers)
        {
            _parsers = parsers;
            _settings = new XmlReaderSettings()
            {
                IgnoreComments = true,
                IgnoreWhitespace = true
            };
        }
        #endregion

        #region Public methods
        public IEnumerable<LibraryEntity> ReadEntities(Stream source)
        {
            using (var xmlReader = XmlReader.Create(source, _settings))
            {
                xmlReader.ReadStartElement();

                while (xmlReader.Read())
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
            var parser = _parsers.FirstOrDefault(x => x.ElementTag == node.Name);
            if (parser == null) throw new Exception($"Unexpected element: '{node.Name}'");
            return parser.Parse(node);
        }
        #endregion
    }
}

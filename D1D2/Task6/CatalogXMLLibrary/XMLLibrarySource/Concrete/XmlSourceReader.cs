using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using CatalogXMLLibrary.Domain.Models;
using CatalogXMLLibrary.XMLLibrarySource.Interfaces;

namespace CatalogXMLLibrary.XMLLibrarySource.Concrete
{
    public class XmlSourceReader : IXmlSourceReader
    {
        private const string _libraryTagName = "library";

        public IEnumerable<LibraryEntity> ReadEntities(Stream source)
        {
            using (var xmlReader = XmlReader.Create(source, GetSettings()))
            {
                xmlReader.ReadStartElement();

                while (xmlReader.Read())
                {
                    if(xmlReader.NodeType == XmlNodeType.Element)
                    {
                        yield return ParseEntity(xmlReader);
                    }
                }
                                
                //todo
                throw new NotImplementedException();
            }
        }

        private XmlReaderSettings GetSettings()
        {
            return new XmlReaderSettings()
            {
                IgnoreComments = true,
                IgnoreWhitespace = true
            };
        }

        private LibraryEntity ParseEntity(XmlReader xmlReader)
        {
            var node = XNode.ReadFrom(xmlReader) as XElement;
            //todo
            return null;
        }
    }
}

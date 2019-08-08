using System;
using System.IO;
using System.Linq;
using System.Xml;
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
        private readonly XmlReaderSettings _settings = new XmlReaderSettings()
        {
            IgnoreComments = true,
            IgnoreWhitespace = true
        };
        #endregion

        #region Constructors
        public XmlSourceReader()
        {
        }

        public XmlSourceReader(ICollection<IXmlElementSerializer> serializers)
        {
            Serializers = serializers;
        }
        #endregion

        #region Properties
        public ICollection<IXmlElementSerializer> Serializers { get; set; }
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
                        var elementTag = xmlReader.LocalName;
                        var element = xmlReader.ReadOuterXml();
                        yield return DeserializeEntity(element, elementTag);
                    }
                }
            }
        }
        #endregion

        #region Private methods
        private LibraryEntity DeserializeEntity(string element, string elementTag)
        {
            var serializer = Serializers.FirstOrDefault(x => x.ElementTag == elementTag);
            if (serializer == null) throw new Exception($"Unexpected element: '{elementTag}'. Reader doesn't have serializer for this element!");
            return serializer.Deserialize(element);
        }
        #endregion
    }
}

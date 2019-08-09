using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using CatalogXMLLibrary.Domain.Models;
using CatalogXMLLibrary.XMLLibrarySource.Interfaces;
using System.Text;

namespace CatalogXMLLibrary.XMLLibrarySource.Concrete
{
    public class XmlSourceWriter : IXmlSourceWriter
    {
        #region Constants
        private const string _libraryTagName = "library";
        XmlWriterSettings _settings = new XmlWriterSettings { Indent = true, CheckCharacters = false };
        #endregion

        #region Constructors
        public XmlSourceWriter()
        {
        }

        public XmlSourceWriter(ICollection<IXmlElementSerializer> serializers)
        {
            Serializers = serializers;
        }
        #endregion

        #region Properties
        public ICollection<IXmlElementSerializer> Serializers { get; set; }
        #endregion

        #region Public methods
        public void WriteEntities(Stream source, IEnumerable<LibraryEntity> entities)
        {
            using (var xmlWriter = XmlWriter.Create(source, _settings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement(_libraryTagName);
                foreach(var entity in entities)
                {
                    var xmlEntity = SerializeEntity(xmlWriter, entity);
                    xmlWriter.WriteRaw($"{Environment.NewLine} {xmlEntity}");
                }
                xmlWriter.WriteRaw($"{Environment.NewLine}");
                xmlWriter.WriteEndElement();
            }
        }
        #endregion

        #region Private methods
        private string SerializeEntity(XmlWriter xmlWriter, LibraryEntity entity)
        {
            var serializer = Serializers.FirstOrDefault(x => x.ElementType == entity.GetType());
            if (serializer == null) throw new Exception($"Unexpected element: '{entity.GetType()}'. Serializer doesn't exist for this type!");
            return serializer.Serialize(entity);
        }
        #endregion
    }
}

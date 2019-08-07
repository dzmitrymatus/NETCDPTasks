using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using CatalogXMLLibrary.Domain.Models;
using CatalogXMLLibrary.XMLLibrarySource.Concrete.XmlElementWriters;
using CatalogXMLLibrary.XMLLibrarySource.Interfaces;

namespace CatalogXMLLibrary.XMLLibrarySource.Concrete
{
    public class XmlSourceWriter : IXmlSourceWriter
    {
        #region Constants
        private const string _libraryTagName = "library";
        XmlWriterSettings _settings = new XmlWriterSettings { Indent = true };
        #endregion

        #region Constructors
        public XmlSourceWriter()
        {
            Writers = new List<IXmlElementWriter>()
            {
                new BookElementWriter(),
                new NewspaperElementWriter(),
                new PatentElementWriter()
            };
        }

        public XmlSourceWriter(ICollection<IXmlElementWriter> writers)
        {
            Writers = writers;
        }
        #endregion

        #region Properties
        public ICollection<IXmlElementWriter> Writers { get; set; }
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
                    WriteEntity(xmlWriter, entity);
                }
                xmlWriter.WriteEndElement();
            }
        }
        #endregion

        #region Private methods
        private void WriteEntity(XmlWriter xmlWriter, LibraryEntity entity)
        {
            var writer = Writers.FirstOrDefault(x => x.ElementType == entity.GetType());
            if (writer == null) throw new Exception($"Unexpected element: '{entity.GetType()}'. Writer doesn't exist for this type!");
            writer.Write(xmlWriter, entity);
        }
        #endregion
    }
}

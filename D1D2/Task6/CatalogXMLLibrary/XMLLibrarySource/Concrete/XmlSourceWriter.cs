using System.IO;
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

        public void WriteEntities(Stream source, IEnumerable<LibraryEntity> entities)
        {
            //todo
            throw new System.NotImplementedException();
        }
    }
}

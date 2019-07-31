using CatalogXMLLibrary.Domain.Models;
using CatalogXMLLibrary.XMLLibrarySource.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace CatalogXMLLibrary.XMLLibrarySource.Concrete
{
    public class XmlSourceWriter : IXmlSourceWriter
    {
        #region Constants
        private const string _libraryTagName = "library";
        #endregion

        #region Fields
        private IEnumerable<IXmlElementWriter> _writers;
        #endregion

        public void WriteEntities(Stream source, IEnumerable<LibraryEntity> entities)
        {
            //todo
            throw new System.NotImplementedException();
        }
    }
}

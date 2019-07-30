using CatalogXMLLibrary.Domain.Models;
using CatalogXMLLibrary.XMLLibrarySource.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace CatalogXMLLibrary.XMLLibrarySource.Concrete
{
    public class XmlSourceWriter : IXmlSourceWriter
    {
        public void WriteEntities(Stream source, IEnumerable<LibraryEntity> entities)
        {
            throw new System.NotImplementedException();
        }
    }
}

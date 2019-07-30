using System.Collections.Generic;
using System.IO;
using CatalogXMLLibrary.Domain.Models;

namespace CatalogXMLLibrary.XMLLibrarySource.Interfaces
{
    public interface IXmlSourceWriter
    {
        void WriteEntities(Stream source, IEnumerable<LibraryEntity> entities);
    }
}

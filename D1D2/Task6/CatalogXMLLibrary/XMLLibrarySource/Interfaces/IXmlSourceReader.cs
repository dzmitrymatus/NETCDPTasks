using CatalogXMLLibrary.Domain.Models;
using System.Collections.Generic;
using System.IO;

namespace CatalogXMLLibrary.XMLLibrarySource.Interfaces
{
    public interface IXmlSourceReader
    {
        IEnumerable<LibraryEntity> ReadEntities(Stream source);
    }
}

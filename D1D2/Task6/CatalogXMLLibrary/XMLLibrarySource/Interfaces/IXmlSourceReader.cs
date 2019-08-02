using CatalogXMLLibrary.Domain.Models;
using System.Collections.Generic;
using System.IO;

namespace CatalogXMLLibrary.XMLLibrarySource.Interfaces
{
    public interface IXmlSourceReader
    {
        ICollection<IXmlElementParser> Parsers { get; set; }
        IEnumerable<LibraryEntity> ReadEntities(Stream source);
    }
}

using System.IO;
using System.Collections.Generic;
using CatalogXMLLibrary.Domain.Models;

namespace CatalogXMLLibrary.XMLLibrarySource.Interfaces
{
    public interface IXmlSourceReader
    {
        ICollection<IXmlElementSerializer> Serializers { get; set; }
        IEnumerable<LibraryEntity> ReadEntities(Stream source);
    }
}

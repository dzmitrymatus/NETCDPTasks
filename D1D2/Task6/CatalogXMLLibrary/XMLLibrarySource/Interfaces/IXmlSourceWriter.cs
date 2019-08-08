using System.IO;
using System.Collections.Generic;
using CatalogXMLLibrary.Domain.Models;

namespace CatalogXMLLibrary.XMLLibrarySource.Interfaces
{
    public interface IXmlSourceWriter
    {
        ICollection<IXmlElementSerializer> Serializers { get; set; }
        void WriteEntities(Stream source, IEnumerable<LibraryEntity> entities);
    }
}

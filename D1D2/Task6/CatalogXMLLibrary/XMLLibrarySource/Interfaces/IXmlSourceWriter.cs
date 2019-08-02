using System.Collections.Generic;
using System.IO;
using CatalogXMLLibrary.Domain.Models;

namespace CatalogXMLLibrary.XMLLibrarySource.Interfaces
{
    public interface IXmlSourceWriter
    {
        ICollection<IXmlElementWriter> Writers { get; set; }
        void WriteEntities(Stream source, IEnumerable<LibraryEntity> entities);
    }
}

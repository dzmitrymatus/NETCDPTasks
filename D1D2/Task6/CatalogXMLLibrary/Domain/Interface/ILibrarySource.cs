using System.Collections.Generic;
using CatalogXMLLibrary.Domain.Models;

namespace CatalogXMLLibrary.Domain.Interface
{
    public interface ILibrarySource
    {
        IEnumerable<LibraryEntity> Read();
        void Write(IEnumerable<LibraryEntity> libraryEntities);
    }
}

using System;
using CatalogXMLLibrary.Domain.Models;

namespace CatalogXMLLibrary.XMLLibrarySource.Interfaces
{
    public interface IXmlElementSerializer
    {
        string ElementTag { get; }
        Type ElementType { get; }
        LibraryEntity Deserialize(string element);
        string Serialize(LibraryEntity entity);
    }
}

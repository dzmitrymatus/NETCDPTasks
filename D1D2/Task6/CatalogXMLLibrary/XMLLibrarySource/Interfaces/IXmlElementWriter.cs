using System;
using CatalogXMLLibrary.Domain.Models;

namespace CatalogXMLLibrary.XMLLibrarySource.Interfaces
{
    public interface IXmlElementWriter
    {
        Type ElementType { get; }
        void Write(LibraryEntity entity);
    }
}

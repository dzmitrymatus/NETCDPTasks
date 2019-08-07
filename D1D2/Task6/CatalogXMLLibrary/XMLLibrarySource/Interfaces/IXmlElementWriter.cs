using System;
using System.Xml;
using CatalogXMLLibrary.Domain.Models;

namespace CatalogXMLLibrary.XMLLibrarySource.Interfaces
{
    public interface IXmlElementWriter
    {
        Type ElementType { get; }
        void Write(XmlWriter xmlWriter, LibraryEntity entity);
    }
}

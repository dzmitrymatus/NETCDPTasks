using CatalogXMLLibrary.Domain.Models;
using CatalogXMLLibrary.Domain.Models.LibraryEntities;
using CatalogXMLLibrary.XMLLibrarySource.Interfaces;
using System;
using System.Xml;

namespace CatalogXMLLibrary.XMLLibrarySource.Concrete.XmlElementWriters
{
    public class NewspaperElementWriter : IXmlElementWriter
    {
        public Type ElementType => typeof(Newspaper);

        public void Write(XmlWriter xmlWriter, LibraryEntity entity)
        {
           //todo
        }
    }
}

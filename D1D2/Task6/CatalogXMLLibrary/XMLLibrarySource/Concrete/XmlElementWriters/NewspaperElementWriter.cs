using CatalogXMLLibrary.Domain.Models;
using CatalogXMLLibrary.Domain.Models.LibraryEntities;
using CatalogXMLLibrary.XMLLibrarySource.Interfaces;
using System;

namespace CatalogXMLLibrary.XMLLibrarySource.Concrete.XmlElementWriters
{
    public class NewspaperElementWriter : IXmlElementWriter
    {
        public Type ElementType => typeof(Newspaper);

        public void Write(LibraryEntity entity)
        {
            //todo
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Xml.Linq;
using CatalogXMLLibrary.Domain.Models;
using CatalogXMLLibrary.XMLLibrarySource.Interfaces;

namespace CatalogXMLLibrary.XMLLibrarySource.Concrete.XmlElementParsers
{
    public class BookElementParser : IXmlElementParser
    {
        public string ElementTag => "book";

        public LibraryEntity Parse(XElement element)
        {
            //todo
            throw new NotImplementedException();
        }
    }
}

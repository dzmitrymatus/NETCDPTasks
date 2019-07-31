using System;
using System.Xml.Linq;
using CatalogXMLLibrary.Domain.Models;
using CatalogXMLLibrary.XMLLibrarySource.Interfaces;

namespace CatalogXMLLibrary.XMLLibrarySource.Concrete.XmlElementParsers
{
    public class NewspaperElementParser : IXmlElementParser
    {
        public string ElementTag => "newspaper";

        public LibraryEntity Parse(XElement element)
        {
            //todo
            throw new NotImplementedException();
        }
    }
}

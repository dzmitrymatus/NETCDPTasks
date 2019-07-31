using System;
using System.Xml.Linq;
using CatalogXMLLibrary.Domain.Models;
using CatalogXMLLibrary.XMLLibrarySource.Interfaces;

namespace CatalogXMLLibrary.XMLLibrarySource.Concrete.XmlElementParsers
{
    class PatentElementParser : IXmlElementParser
    {
        public string ElementTag => "patent";

        public LibraryEntity Parse(XElement element)
        {
            //todo
            throw new NotImplementedException();
        }
    }
}

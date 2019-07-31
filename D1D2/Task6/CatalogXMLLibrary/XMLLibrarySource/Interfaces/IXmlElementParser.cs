using System.Xml.Linq;
using CatalogXMLLibrary.Domain.Models;

namespace CatalogXMLLibrary.XMLLibrarySource.Interfaces
{
    public interface IXmlElementParser
    {
        string ElementTag { get; }
        LibraryEntity Parse(XElement element);
    }
}

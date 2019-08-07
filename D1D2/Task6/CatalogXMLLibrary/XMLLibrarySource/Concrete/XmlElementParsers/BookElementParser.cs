using System.Xml.Linq;
using CatalogXMLLibrary.Domain.Models;
using CatalogXMLLibrary.Domain.Models.LibraryEntities;
using CatalogXMLLibrary.XMLLibrarySource.Concrete.XmlElementsTags;
using CatalogXMLLibrary.XMLLibrarySource.Interfaces;

namespace CatalogXMLLibrary.XMLLibrarySource.Concrete.XmlElementParsers
{
    public class BookElementParser : IXmlElementParser
    {
        public string ElementTag => "book";

        public LibraryEntity Parse(XElement element)
        {
            var book = new Book()
            {
                Author = element.Element(BookElementTags.AuthorTag)?.Value,
                City = element.Element(BookElementTags.CityTag)?.Value,
                Name = element.Element(BookElementTags.NameTag)?.Value,
                Notice = element.Element(BookElementTags.NoticeTag)?.Value,
                ISBN = element.Element(BookElementTags.IsbnTag)?.Value,
                PagesNumber = int.TryParse(element.Element(BookElementTags.PagesNumberTag)?.Value, out var i) ? i : (int?)null,
                Year = int.TryParse(element.Element(BookElementTags.YearTag)?.Value, out var y) ? y : (int?)null
            };
            return book;
        }
    }
}

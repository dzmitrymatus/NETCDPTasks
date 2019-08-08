using System;
using System.Xml.Linq;
using CatalogXMLLibrary.Domain.Models;
using CatalogXMLLibrary.Domain.Models.LibraryEntities;
using CatalogXMLLibrary.XMLLibrarySource.Interfaces;

namespace CatalogXMLLibrary.XMLLibrarySource.Concrete.XmlElementSerializers
{
    public class BookElementSerializer : IXmlElementSerializer
    {
        private const string _authorTag = "author";
        private const string _cityTag = "city";
        private const string _nameTag = "name";
        private const string _noticeTag = "notice";
        private const string _isbnTag = "isbn";
        private const string _pagesNumberTag = "pagesNumber";
        private const string _yearTag = "year";

        public string ElementTag => "book";
        public Type ElementType => typeof(Book);

        public string Serialize(LibraryEntity entity)
        {
            var book = entity as Book;
            if (book == null) throw new NullReferenceException();

            var bookXml = new XElement(ElementTag,
                new XElement(_authorTag, book.Author),
                new XElement(_cityTag, book.City),
                new XElement(_nameTag, book.Name),
                new XElement(_noticeTag, book.Notice),
                new XElement(_isbnTag, book.ISBN),
                new XElement(_pagesNumberTag, book.PagesNumber?.ToString()),
                new XElement(_yearTag, book.Year?.ToString()));
            return bookXml.ToString(SaveOptions.None);            
        }

        public LibraryEntity Deserialize(string elementXml)
        {
            var element = XElement.Parse(elementXml);
            var book = new Book()
            {
                Author = element.Element(_authorTag)?.Value,
                City = element.Element(_cityTag)?.Value,
                Name = element.Element(_nameTag)?.Value,
                Notice = element.Element(_noticeTag)?.Value,
                ISBN = element.Element(_isbnTag)?.Value,
                PagesNumber = int.TryParse(element.Element(_pagesNumberTag)?.Value, out var i) ? i : (int?)null,
                Year = int.TryParse(element.Element(_yearTag)?.Value, out var y) ? y : (int?)null
            };
            return book;
        }
    }
}

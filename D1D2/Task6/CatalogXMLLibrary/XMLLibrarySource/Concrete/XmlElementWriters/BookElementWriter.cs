using System;
using System.Xml;
using CatalogXMLLibrary.Domain.Models;
using CatalogXMLLibrary.Domain.Models.LibraryEntities;
using CatalogXMLLibrary.XMLLibrarySource.Concrete.XmlElementsTags;
using CatalogXMLLibrary.XMLLibrarySource.Interfaces;

namespace CatalogXMLLibrary.XMLLibrarySource.Concrete.XmlElementWriters
{
    public class BookElementWriter : IXmlElementWriter
    {
        public Type ElementType => typeof(Book);

        public void Write(XmlWriter xmlWriter, LibraryEntity entity)
        {
            var book = entity as Book;
            if (book == null) throw new NullReferenceException();
            xmlWriter.WriteStartElement("book");
            xmlWriter.WriteElementString(BookElementTags.NameTag, book.Name);
            xmlWriter.WriteElementString(BookElementTags.AuthorTag, book.Author);
            xmlWriter.WriteElementString(BookElementTags.CityTag, book.City);
            xmlWriter.WriteElementString(BookElementTags.YearTag, book.Year?.ToString());
            xmlWriter.WriteElementString(BookElementTags.PagesNumberTag, book.PagesNumber?.ToString());
            xmlWriter.WriteElementString(BookElementTags.NoticeTag, book.Notice);
            xmlWriter.WriteElementString(BookElementTags.IsbnTag, book.ISBN);
            xmlWriter.WriteEndElement();
        }
    }
}

using System;
using System.Xml.Linq;
using CatalogXMLLibrary.Domain.Models;
using CatalogXMLLibrary.Domain.Models.LibraryEntities;
using CatalogXMLLibrary.XMLLibrarySource.Interfaces;

namespace CatalogXMLLibrary.XMLLibrarySource.Concrete.XmlElementParsers
{
    public class NewspaperElementParser : IXmlElementParser
    {
        private const string _publisherNameTag = "publisherName";
        private const string _cityTag = "city";
        private const string _nameTag = "name";
        private const string _noticeTag = "notice";
        private const string _issnTag = "issn";
        private const string _issueTag = "issue";
        private const string _yearTag = "year";
        private const string _pagesNumberTag = "pagesNumber";

        public string ElementTag => "newspaper";

        public LibraryEntity Parse(XElement element)
        {
            var newspaper = new Newspaper()
            {
                PublisherName = element.Element(_publisherNameTag)?.Value,
                City = element.Element(_cityTag)?.Value,
                Name = element.Element(_nameTag)?.Value,
                Notice = element.Element(_noticeTag)?.Value,
                ISSN = element.Element(_issnTag)?.Value,
                Issue = int.TryParse(element.Element(_issueTag)?.Value, out var i) ? i : (int?)null,
                Year = int.TryParse(element.Element(_yearTag)?.Value, out var y) ? y : (int?)null,
                PagesNumber = int.TryParse(element.Element(_pagesNumberTag)?.Value, out var p) ? p : (int?)null,
                Date = // todo
            };
            return newspaper;
        }
    }
}

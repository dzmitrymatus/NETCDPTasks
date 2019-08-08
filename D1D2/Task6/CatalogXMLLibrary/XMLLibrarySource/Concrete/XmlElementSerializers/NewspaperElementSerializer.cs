using System;
using System.Globalization;
using System.Xml.Linq;
using CatalogXMLLibrary.Domain.Models;
using CatalogXMLLibrary.Domain.Models.LibraryEntities;
using CatalogXMLLibrary.XMLLibrarySource.Interfaces;

namespace CatalogXMLLibrary.XMLLibrarySource.Concrete.XmlElementSerializers
{
    public class NewspaperElementSerializer : IXmlElementSerializer
    {
        private const string _publisherNameTag = "publisherName";
        private const string _cityTag = "city";
        private const string _nameTag = "name";
        private const string _noticeTag = "notice";
        private const string _issnTag = "issn";
        private const string _issueTag = "issue";
        private const string _yearTag = "year";
        private const string _pagesNumberTag = "pagesNumber";
        private const string _dateTag = "date";

        public string ElementTag => "newspaper";
        public Type ElementType => typeof(Newspaper);

        public string Serialize(LibraryEntity entity)
        {
            var newspaper = entity as Newspaper;
            if (newspaper == null) throw new NullReferenceException();

            var newspaperXml = new XElement(ElementTag,
                new XElement(_publisherNameTag, newspaper.PublisherName),
                new XElement(_cityTag, newspaper.City),
                new XElement(_nameTag, newspaper.Name),
                new XElement(_noticeTag, newspaper.Notice),
                new XElement(_issnTag, newspaper.ISSN),
                new XElement(_issueTag, newspaper.Issue?.ToString()),
                new XElement(_yearTag, newspaper.Year?.ToString()),
                new XElement(_pagesNumberTag, newspaper.PagesNumber?.ToString()),
                new XElement(_dateTag, newspaper.Date?.ToString()));
            return newspaperXml.ToString();
        }

        public LibraryEntity Deserialize(string elementXml)
        {
            var element = XElement.Parse(elementXml);
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
                Date = DateTime.TryParseExact(element.Element(_dateTag)?.Value, "mm-dd", null, DateTimeStyles.None, out var d) ? d : (DateTime?)null,
            };
            return newspaper;
        }
    }
}

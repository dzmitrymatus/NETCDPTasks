using System;
using System.Globalization;
using System.Xml.Linq;
using CatalogXMLLibrary.Domain.Models;
using CatalogXMLLibrary.Domain.Models.LibraryEntities;
using CatalogXMLLibrary.XMLLibrarySource.Interfaces;

namespace CatalogXMLLibrary.XMLLibrarySource.Concrete.XmlElementSerializers
{
    public class PatentElementSerializer : IXmlElementSerializer
    {
        private const string _nameTag = "name";
        private const string _inventorTag = "inventor";
        private const string _countryTag = "country";
        private const string _registrationNumberTag = "registrationNumber";
        private const string _applicationDateTag = "applicationDate";
        private const string _publicationDateTag = "publicationDate";
        private const string _pagesNumberTag = "pagesNumber";
        private const string _noticeTag = "notice";

        public string ElementTag => "patent";
        public Type ElementType => typeof(Patent);

        public string Serialize(LibraryEntity entity)
        {
            var patent = entity as Patent;
            if (patent == null) throw new NullReferenceException();

            var patentXml = new XElement(ElementTag,
                new XElement(_nameTag, patent.Name),
                new XElement(_inventorTag, patent.Inventor),
                new XElement(_countryTag, patent.Country),
                new XElement(_registrationNumberTag, patent.RegistrationNumber?.ToString()),
                new XElement(_applicationDateTag, patent.ApplicationDate?.ToString("yyyy-mm-dd")),
                new XElement(_publicationDateTag, patent.PublicationDate?.ToString("yyyy-mm-dd")),
                new XElement(_pagesNumberTag, patent.PagesNumber?.ToString()),
                new XElement(_noticeTag, patent.Notice),
                new XText($"{Environment.NewLine} "));
            return patentXml.ToString();
        }

        public LibraryEntity Deserialize(string elementXml)
        {
            var element = XElement.Parse(elementXml);
            var patent = new Patent()
            {
                Name = element.Element(_nameTag)?.Value,
                Inventor = element.Element(_inventorTag)?.Value,
                Country = element.Element(_countryTag)?.Value,
                RegistrationNumber = int.TryParse(element.Element(_registrationNumberTag)?.Value, out var rn) ? rn : (int?)null,
                ApplicationDate = DateTime.TryParseExact(element.Element(_applicationDateTag)?.Value, "yyyy-mm-dd", null, DateTimeStyles.None, out var ad) ? ad : (DateTime?)null,
                PublicationDate = DateTime.TryParseExact(element.Element(_publicationDateTag)?.Value, "yyyy-mm-dd", null, DateTimeStyles.None, out var pd) ? pd : (DateTime?)null,
                PagesNumber = int.TryParse(element.Element(_pagesNumberTag)?.Value, out var pn) ? pn : (int?)null,
                Notice = element.Element(_noticeTag)?.Value
            };
            return patent;
        }
    }
}

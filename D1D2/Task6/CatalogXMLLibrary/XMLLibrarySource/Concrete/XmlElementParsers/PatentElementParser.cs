using System;
using System.Globalization;
using System.Xml.Linq;
using CatalogXMLLibrary.Domain.Models;
using CatalogXMLLibrary.Domain.Models.LibraryEntities;
using CatalogXMLLibrary.XMLLibrarySource.Interfaces;

namespace CatalogXMLLibrary.XMLLibrarySource.Concrete.XmlElementParsers
{
    public class PatentElementParser : IXmlElementParser
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

        public LibraryEntity Parse(XElement element)
        {
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

using System;
using System.Collections.Generic;
using CatalogXMLLibrary.Domain.Models;
using CatalogXMLLibrary.Domain.Models.LibraryEntities;

namespace CatalogXMLLibrary.UnitTests.TestData
{
    public class XmlDataSource
    {
        public static string RawXmlData =>
 @"<?xml version=""1.0"" encoding=""utf-8""?>
<library>
  <book>
    <name>Example book 1</name>
    <author>Example author 1</author>
    <city>Example city 1</city>
    <year>2019</year>
    <pagesNumber>30</pagesNumber>
    <notice>Example notice 1</notice>
    <isbn>123432423423</isbn>
  </book>
  <newspaper>
    <name>Example newspaper 1</name>
    <city>Example city 1</city>
    <publisherName>Example publisher 1</publisherName>
    <year>2019</year>
    <pagesNumber>45</pagesNumber>
    <notice>Example notice 1</notice>
    <issue>1</issue>
    <date>07-30</date>
    <issn>435345345345</issn>
  </newspaper>
  <patent>
    <name>Example patent 1</name>
    <inventor>Example inventor 1</inventor>
    <country>Example country 1</country>
    <registrationNumber>23</registrationNumber>
    <applicationDate>2019-07-25</applicationDate>
    <publicationDate>2019-07-25</publicationDate>
    <pagesNumber>56</pagesNumber>
    <notice>Example notice 1</notice>
  </patent>
</library>";

        public static IEnumerable<LibraryEntity> XmlEntities
        {
            get
            {
                yield return new Book()
                {
                    Name = "Example book 1",
                    Author = "Example author 1",
                    City = "Example city 1",
                    Year = 2019,
                    PagesNumber = 30,
                    Notice = "Example notice 1",
                    ISBN = "123432423423"
                };
                yield return new Newspaper()
                {
                    Name = "Example newspaper 1",
                    City = "Example city 1",
                    PublisherName = "Example publisher 1",
                    Year = 2019,
                    PagesNumber = 45,
                    Notice = "Example notice 1",
                    Issue = 1,
                    Date = DateTime.ParseExact("07-30", "mm-dd", null),
                    ISSN = "435345345345"
                };
                yield return new Patent()
                {
                    Name = "Example patent 1",
                    Inventor = "Example inventor 1",
                    Country = "Example country 1",
                    RegistrationNumber = 23,
                    ApplicationDate = DateTime.ParseExact("2019-07-25", "yyyy-mm-dd", null),
                    PublicationDate = DateTime.ParseExact("2019-07-25", "yyyy-mm-dd", null),
                    PagesNumber = 56,
                    Notice = "Example notice 1"
                };
            }
        }
    }
}

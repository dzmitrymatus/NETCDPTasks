using System;

namespace CatalogXMLLibrary.Domain.Models.LibraryEntities
{
    public class Newspaper : LibraryEntity
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string PublisherName { get; set; }
        public DateTime Year { get; set; }
        public int PagesNumber { get; set; }
        public string Notice { get; set; }
        public int Issue { get; set; }
        public DateTime Date { get; set; }
        public string ISSN { get; set; }
    }
}

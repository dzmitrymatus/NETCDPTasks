using System;

namespace CatalogXMLLibrary.Domain.Models.LibraryEntities
{
    public class Book : LibraryEntity
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string City { get; set; }
        public DateTime Year { get; set; }
        public int PagesNumber { get; set; }
        public string Notice { get; set; }
        public string ISBN { get; set; }
    }
}

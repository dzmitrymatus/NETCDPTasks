using System;

namespace CatalogXMLLibrary.Domain.Models.LibraryEntities
{
    public class Patent : LibraryEntity
    {
        public string Name { get; set; }
        public string Inventor { get; set; }
        public string Country { get; set; }
        public int RegistrationNumber { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime PublicationDate { get; set; }
        public int PagesNumber { get; set; }
        public string Notice { get; set; }
    }
}

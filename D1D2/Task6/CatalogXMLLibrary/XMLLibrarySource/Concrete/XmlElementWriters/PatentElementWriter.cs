﻿using System;
using CatalogXMLLibrary.Domain.Models;
using CatalogXMLLibrary.Domain.Models.LibraryEntities;
using CatalogXMLLibrary.XMLLibrarySource.Interfaces;

namespace CatalogXMLLibrary.XMLLibrarySource.Concrete.XmlElementWriters
{
    public class PatentElementWriter : IXmlElementWriter
    {
        public Type ElementType => typeof(Patent);

        public void Write(LibraryEntity entity)
        {
            //todo
            throw new NotImplementedException();
        }
    }
}

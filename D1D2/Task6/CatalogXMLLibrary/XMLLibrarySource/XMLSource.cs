using CatalogXMLLibrary.Domain.Interface;
using CatalogXMLLibrary.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace CatalogXMLLibrary.XMLLibrarySource
{
    public class XMLSource : ILibrarySource
    {
        #region Fields
        private Stream _source;
        #endregion

        #region Constructors
        public XMLSource(Stream source)
        {
            _source = source;
        }
        #endregion

        #region Methods
        public IEnumerable<LibraryEntity> Read()
        {
            throw new NotImplementedException();
        }

        public void Write(IEnumerable<LibraryEntity> libraryEntities)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

﻿using System.IO;
using System.Collections.Generic;
using CatalogXMLLibrary.Domain.Models;
using CatalogXMLLibrary.Domain.Interface;
using CatalogXMLLibrary.XMLLibrarySource.Concrete;
using CatalogXMLLibrary.XMLLibrarySource.Interfaces;

namespace CatalogXMLLibrary.XMLLibrarySource
{
    public class XmlSource : ILibrarySource
    {
        #region Fields
        private Stream _source;
        private IXmlSourceReader _reader;
        private IXmlSourceWriter _writer;
        #endregion

        #region Constructors
        public XmlSource(Stream source)
        {
            _source = source;
            _reader = new XmlSourceReader();
            _writer = new XmlSourceWriter();
        }

        public XmlSource(Stream source, IXmlSourceReader reader, IXmlSourceWriter writer)
        {
            _source = source;
            _reader = reader;
            _writer = writer;
        }
        #endregion

        #region Methods
        public IEnumerable<LibraryEntity> Read()
        {
            return _reader.ReadEntities(_source);            
        }

        public void Write(IEnumerable<LibraryEntity> libraryEntities)
        {
            _writer.WriteEntities(_source, libraryEntities);
        }
        #endregion
    }
}

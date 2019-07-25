using CatalogXMLLibrary.Domain.Interface;
using CatalogXMLLibrary.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace CatalogXMLLibrary.XMLLibrarySource
{
    public class XMLSource : ILibrarySource
    {
        #region Constants
        private const string _libraryTagName = "library";
        #endregion

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
            var xmlReaderSettings = new XmlReaderSettings()
            {
                IgnoreComments = true,
                IgnoreWhitespace = true
            };
            using (var xmlReader = XmlReader.Create(_source, xmlReaderSettings))
            {
                if (xmlReader.ReadToFollowing(_libraryTagName) == false)
                {
                    return Array.Empty<LibraryEntity>();
                }
                else
                {
                    xmlReader.ReadStartElement();
                }
                //todo
            }
            throw new NotImplementedException();
        }

        public void Write(IEnumerable<LibraryEntity> libraryEntities)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

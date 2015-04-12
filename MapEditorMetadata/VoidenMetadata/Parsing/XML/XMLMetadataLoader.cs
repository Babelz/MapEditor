using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MapEditor.Metadata.Parsing.XML
{
    public sealed class XMLMetadataLoader : MetadataLoader
    {
        #region Fields
        private readonly XMLMetadataParser parser;
        private readonly XMLMetadataReader reader;
        #endregion

        #region Properties
        public override string FileFormat
        {
            get
            {
                return XMLInformation.FILE_FORMAT;
            }
        }
        public override string Name
        {
            get
            {
                return XMLInformation.NAME;
            }
        }
        #endregion

        public XMLMetadataLoader()
            : base()
        {
            parser = new XMLMetadataParser();
            reader = new XMLMetadataReader();
        }


        protected override MetadataObjectGroup InternalLoad(string path)
        {
            XDocument document = reader.Read(path);

            return parser.Parse(document);
        }
    }
}

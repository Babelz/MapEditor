using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MapEditor.Metadata.Parsing.XML
{
    public sealed class XMLMetadataReader : MetadataReader<XDocument>
    {
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

        public XMLMetadataReader()
            : base()
        {
        }

        protected override XDocument InternalRead(string path)
        {
            return XDocument.Load(path);
        }
    }
}

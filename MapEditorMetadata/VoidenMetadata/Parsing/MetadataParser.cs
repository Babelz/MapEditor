using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Metadata.Parsing
{
    /// <summary>
    /// Interface for parsing metadata.
    /// </summary>
    /// <typeparam name="T">metadata source</typeparam>
    public abstract class MetadataParser<T>
    {
        #region Properties
        /// <summary>
        /// File format that this parser accepts.
        /// </summary>
        public abstract string FileFormat
        {
            get;
        }
        /// <summary>
        /// Just for keeping track of the parsers. There could be 
        /// several parsers for one file format.
        /// </summary>
        public abstract string Name
        {
            get;
        }
        #endregion

        public MetadataParser()
        {
        }

        protected abstract MetadataObjectGroup InternalParse(T file);

        /// <summary>
        /// Parses metadata from given file.
        /// </summary>
        /// <param name="file">file to parse</param>
        /// <returns>group</returns>
        public MetadataObjectGroup Parse(T file)
        {
            return InternalParse(file);
        }
    }
}

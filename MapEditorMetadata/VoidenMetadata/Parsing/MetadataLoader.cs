using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Metadata.Parsing
{
    /// <summary>
    /// Non generic interface for loading metadatas.
    /// </summary>
    public abstract class MetadataLoader
    {
        #region Properties
        /// <summary>
        /// File format that this loader can load.
        /// </summary>
        public abstract string FileFormat
        {
            get;
        }
        /// <summary>
        /// Just for keeping track of the loaders. There could be...
        /// </summary>
        public abstract string Name
        {
            get;
        }
        #endregion

        public MetadataLoader()
        {
        }

        protected abstract MetadataObjectGroup InternalLoad(string path);

        /// <summary>
        /// Loads metadata from a file.
        /// </summary>
        /// <param name="path">metadata files path</param>
        /// <returns>group that contains parsed metadata</returns>
        /// <exception cref="MetadataParserException">Invalid file format.</exception>
        public MetadataObjectGroup Load(string path)
        {
            if (!path.EndsWith(FileFormat))
            {
                // Invalid file format. 
                throw MetadataParserException.InvalidFormat(Name, path.Split('.').Last(), FileFormat);
            }

            return InternalLoad(path);
        }
    }
}

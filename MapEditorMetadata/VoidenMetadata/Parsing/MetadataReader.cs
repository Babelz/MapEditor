using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Metadata.Parsing
{
    /// <summary>
    /// Interface for reading and preprocessing 
    /// metadata files.
    /// </summary>
    /// <typeparam name="T">type of the file, can be string, xml file etc</typeparam>
    public abstract class MetadataReader<T>
    {
        #region Properties
        /// <summary>
        /// File format that this reader is supposed to read.
        /// </summary>
        public abstract string FileFormat
        {
            get;
        }
        /// <summary>
        /// Just for keeping track of the readers. There could be
        /// several readers for one file format.
        /// </summary>
        public abstract string Name
        {
            get;
        }
        #endregion

        public MetadataReader()
        {
        }

        protected abstract T InternalRead(string path);

        /// <summary>
        /// Reads the given file and returns its contents. Can 
        /// do some preprocessing before returning it to the caller.
        /// </summary>
        /// <exception cref="MetadataParserException">Invalid file format.</exception>
        /// <returns>metadata file</returns>
        public T Read(string path)
        {
            if (!path.EndsWith(FileFormat))
            {
                // Invalid format.
                throw MetadataParserException.InvalidFormat(Name, path.Split('.').Last(), FileFormat);
            }

            return InternalRead(path);
        }
    }
}

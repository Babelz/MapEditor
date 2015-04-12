using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Metadata.Parsing
{
    public sealed class MetadataParserException : Exception 
    {
        private MetadataParserException(string message)
            : base("Metadata exception: " + message)
        {
        }

        /// <summary>
        /// Create new instance for invalid format exception.
        /// </summary>
        /// <param name="loaderName">name of the loader</param>
        /// <param name="given">given format</param>
        /// <param name="excepted">excepted format</param>
        /// <returns>new exception instance</returns>
        public static MetadataParserException InvalidFormat(string loaderName, string given, string excepted)
        {
            return new MetadataParserException(string.Format("invalid file format for {0} loader, got {1}, excepted {2}.", loaderName, given, excepted));
        }

        /// <summary>
        /// Creates new instance for missing attribute exception.
        /// </summary>
        /// <param name="line">line that is missing the attribute</param>
        /// <param name="missingAttributeName">name of the missing attribute</param>
        /// <returns>new exception instance</returns>
        public static MetadataParserException MissingAttribute(string line, string missingAttributeName, string type)
        {
            return new MetadataParserException(string.Format("missing attribute {0} of type {1} at line {3}.", missingAttributeName, type, line));
        }

        public static MetadataParserException InvalidValue(string value, string exceptedType, string line)
        {
            return new MetadataParserException(string.Format("got invalid value at line {0}, excepted type {1}, value is {2}.", line, exceptedType, value));
        }

        public static MetadataParserException FieldNameCollision(string objectsName, string fieldsName)
        {
            return new MetadataParserException(string.Format("object {0} already contains definition for field {1}. ", objectsName, fieldsName));
        }

        public static MetadataParserException ObjectNameCollision(string objectsName)
        {
            return new MetadataParserException(string.Format("{0} is not a unique name.", objectsName));
        }
    }
}

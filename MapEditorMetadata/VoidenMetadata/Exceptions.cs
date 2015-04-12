using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Metadata
{
        public sealed class MetadataSystemException : Exception
        {
            private MetadataSystemException(string message)
                : base("Metadata exception: " + message)
            {

            }

            /// <summary>
            /// Create new instance for type mismatch exception.
            /// </summary>
            /// <param name="excepted">type excepted</param>
            /// <param name="got">type got</param>
            /// <returns>new exception instance</returns>
            public static MetadataSystemException TypeMismatch(string objectName, string fieldName, Type excepted, Type got)
            {
                return new MetadataSystemException(string.Format("type mismatch occured while reading objects {0} field {1}, excepted {2}, got {3}.", objectName, fieldName, excepted.Name, got.Name));
            }

            /// <summary>
            /// Create new instance for field not found exception.
            /// </summary>
            /// <param name="objectName">objects name</param>
            /// <param name="fieldName">fields name</param>
            /// <returns>new exception instance</returns>
            public static MetadataSystemException FieldNotFound(string objectName, string fieldName)
            {
                return new MetadataSystemException(string.Format("field not found. Object {0} does not contain field named {1}.", objectName, fieldName));
            }

            /// <summary>
            /// Creates new instance for object not found exception.
            /// </summary>
            /// <param name="objectName">name of the object</param>
            /// <param name="groupName">name of the group</param>
            /// <returns>new exception instance</returns>
            public static MetadataSystemException ObjectNotFound(string objectName, string groupName)
            {
                return new MetadataSystemException(string.Format("object with name {0} was not found in group {1}.", objectName, groupName));
            }

            /// <summary>
            /// Creates new instance for group not found exception.
            /// </summary>
            /// <param name="groupName">name of the group</param>
            /// <returns>new exception instance</returns>
            public static MetadataSystemException ObjectGroupFound(string groupName)
            {
                return new MetadataSystemException(string.Format("group with name {0} was not found.", groupName));
            }

            /// <summary>
            /// Creates new instance for name not unique exception.
            /// </summary>
            /// <param name="objectName">name that is not unique</param>
            /// <returns>new exception instance</returns>
            public static MetadataSystemException NameNotUnique(string objectName)
            {
                return new MetadataSystemException(string.Format("name {0} is not unique.", objectName));
            }
        }
}

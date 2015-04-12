using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Metadata
{
    /// <summary>
    /// Container for storing metadata objects.
    /// </summary>
    public sealed class MetadataObjectGroup
    {
        #region Fields
        private readonly MetadataObject[] objects;

        private readonly string[] indices;

        private readonly string name;
        #endregion

        #region Properties
        public string Name
        {
            get
            {
                return name;
            }
        }
        #endregion

        /// <param name="name">name of the group</param>
        /// <param name="objects">objects in this group</param>
        public MetadataObjectGroup(string name, MetadataObject[] objects)
        {
            this.name = name;
            this.objects = objects;

            // Create indices.
            indices = new string[objects.Length];

            for (int i = 0; i < indices.Length; i++) indices[i] = objects[i].Name;
        }

        /// <summary>
        /// Finds object with given name.
        /// </summary>
        /// <param name="name">name of the object</param>
        /// <returns>metadata object</returns>
        /// <exception cref="MetadataSystemException">Invalid name.</exception>
        public MetadataObject GetObject(string objectsName)
        {
            int index = Array.IndexOf<string>(indices, objectsName);

            if (index != -1)
            {
                return objects[index];
            }

            // Invalid name.
            throw MetadataSystemException.ObjectNotFound(objectsName, name);
        }
    }
}

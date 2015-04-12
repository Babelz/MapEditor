using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Metadata.Parsing
{
    /// <summary>
    /// Container for storing uniquely named metadata objects.
    /// </summary>
    public sealed class UniqueMetadataObjectCollection
    {
        #region Fields
        private readonly List<MetadataObject> objects;
        #endregion

        public UniqueMetadataObjectCollection()
        {
            objects = new List<MetadataObject>();
        }

        private bool Contains(MetadataObject metadataObject)
        {
            foreach (MetadataObject other in objects)
            {
                if (metadataObject.Name == other.Name) return true;
            }

            return false;
        }

        public bool Add(MetadataObject metadataObject)
        {
            if (Contains(metadataObject)) return false;
            
            objects.Add(metadataObject);

            return true;
        }
        public bool Remove(MetadataObject metadataObject)
        {
            return objects.Remove(metadataObject);
        }

        public IEnumerable<MetadataObject> Objects()
        {
            return objects;
        }
    }
}

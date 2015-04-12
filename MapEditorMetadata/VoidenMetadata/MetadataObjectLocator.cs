using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Metadata
{
    public sealed class MetadataObjectLocator
    {
        #region Fields
        private readonly MetadataObjectGroup[] groups;

        private readonly string[] indices;
        #endregion

        public MetadataObjectLocator(MetadataObjectGroup[] groups)
        {
            this.groups = groups;

            // Create indices.
            indices = new string[groups.Length];

            for (int i = 0; i < indices.Length; i++) indices[i] = groups[i].Name;
        }

        public MetadataObjectGroup GetGroup(string name)
        {
            int index = Array.IndexOf(indices, name);

            if (index != -1)
            {
                return groups[index];
            }

            // Group not found.
            throw MetadataSystemException.ObjectGroupFound(name);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorViewModels
{
    public sealed class NewTilesetProperties
    {
        #region Fields
        private readonly string[] takenNames;

        public string Name;
        public string Path;

        public int OffsetX;
        public int OffsetY;

        public int TileWidth;
        public int TileHeight;
        #endregion

        public NewTilesetProperties(string[] takenNames)
        {
            this.takenNames = takenNames;
        }

        public bool HasUniqueName()
        {
            if (string.IsNullOrEmpty(Name)) return true;

            for (int i = 0; i < takenNames.Length; i++) if (takenNames[i] == Name) return false;

            return true;
        }

        public bool HasValidProperties()
        {
            return !string.IsNullOrEmpty(Name) && File.Exists(Path) && HasUniqueName() && TileWidth > 0 && TileHeight > 0;
        }
    }
}

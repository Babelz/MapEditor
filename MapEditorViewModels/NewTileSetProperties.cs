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
        public string Name;
        public string Path;

        public int OffSetX;
        public int OffSetY;

        public int TileWidth;
        public int TileHeight;
        #endregion

        public NewTilesetProperties()
        {
        }

        public bool HasValidProperties()
        {
            return !string.IsNullOrEmpty(Name) && !File.Exists(Path) && TileWidth != 0 && TileHeight != 0;
        }
    }
}

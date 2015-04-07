using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorViewModels
{
    public enum MapType
    {
        Tile,
        Object,
        Hexa
    }

    public sealed class NewProjectProperties
    {
        #region Fields
        public string ProjectName;
        public string MapName;

        public MapType MapType;

        public int MapHeight;
        public int MapWidth;

        public int TileHeight;
        public int TileWidth;
        #endregion

        public NewProjectProperties()
        {
        }

        public bool IsValidMapModel()
        {
            return !string.IsNullOrEmpty(ProjectName) && !string.IsNullOrEmpty(MapName) &&
                    MapHeight != 0 && MapWidth != 0;
        }
        public bool IsValidTileMapModel()
        {
            return IsValidMapModel() && TileHeight != 0 && TileWidth != 0;
        }
    }
}

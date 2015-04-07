using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorViewModels
{
    /// <summary>
    /// Map type.
    /// </summary>
    public enum MapType
    {
        /// <summary>
        /// Tile based map.
        /// </summary>
        Tile,

        /// <summary>
        /// Object or texture based map.
        /// </summary>
        Object,

        /// <summary>
        /// Hexa based map.
        /// </summary>
        Hex
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

        /// <summary>
        /// Returns true if all properties are valid for selected map type.
        /// </summary>
        public bool HasValidMapProperties()
        {
            return !string.IsNullOrEmpty(ProjectName) && !string.IsNullOrEmpty(MapName) &&
                    MapHeight != 0 && MapWidth != 0;
        }
        /// <summary>
        /// Returns true if all properties are valid for tile maps.
        /// </summary>
        /// <returns></returns>
        public bool HasValidTileMapProperties()
        {
            return HasValidMapProperties() && TileHeight != 0 && TileWidth != 0;
        }
    }
}

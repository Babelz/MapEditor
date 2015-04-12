using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.TileEditor
{
    /// <summary>
    /// Class for managing and storing tilesets. Handles their removal and
    /// disposing.
    /// </summary>
    public sealed class TilesetManager
    {
        #region Fields
        private readonly Dictionary<string, Tileset> tilesets;

        private Tileset selectedTileset;
        #endregion

        #region Properties
        public IEnumerable<Tileset> Tilesets
        {
            get
            {
                return tilesets.Values;
            }
        }
        public Tileset SelectedTileset
        {
            get
            {
                return selectedTileset;
            }
        }
        #endregion

        public TilesetManager()
        {
            tilesets = new Dictionary<string, Tileset>();
        }

        public void SelectTileset(string name)
        {
            // Clear selection.
            if (!string.IsNullOrEmpty(name))
            {
                selectedTileset = null;

                return;
            }

            selectedTileset = tilesets[name];
        }

        /// <summary>
        /// Adds new tileset.
        /// </summary>
        public void AddTileset(Tileset tileset)
        {
            tilesets.Add(tileset.Name, tileset);
        }

        /// <summary>
        /// Removes and disposes given tileset.
        /// </summary>
        public void RemoveTileset(Tileset tileset)
        {
            tilesets.Remove(tileset.Name);

            if (ReferenceEquals(tileset, selectedTileset)) selectedTileset = null;
        }
    }
}

using MapEditorCore.Abstractions;
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
        public bool HasTilesetSelected
        {
            get
            {
                return selectedTileset != null;
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

        #region Events
        /// <summary>
        /// Called when tileset is added.
        /// </summary>
        public event TilesetEventHandler TilesetAdded;

        /// <summary>
        /// Called when tileset is removed.
        /// </summary>
        public event TilesetEventHandler TilesetRemoved;
        #endregion

        public TilesetManager()
        {
            tilesets = new Dictionary<string, Tileset>();
        }

        #region Event handlers
        private void tileset_Deleting(object sender, TilesetEventArgs e)
        {
            RemoveTileset(e.Tileset);
        }
        #endregion

        public void SelectTileset(string name)
        {
            // Clear selection.
            if (string.IsNullOrEmpty(name))
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
            tileset.Deleting += tileset_Deleting;
            
            tilesets.Add(tileset.Name, tileset);

            if (TilesetAdded != null) TilesetAdded(this, new TilesetEventArgs(tileset));
        }

        /// <summary>
        /// Removes and disposes given tileset.
        /// </summary>
        public void RemoveTileset(Tileset tileset)
        {
            tileset.Deleting -= tileset_Deleting;

            tilesets.Remove(tileset.Name);

            if (ReferenceEquals(tileset, selectedTileset)) selectedTileset = null;

            if (TilesetRemoved != null) TilesetRemoved(this, new TilesetEventArgs(tileset));
        }
    }
}

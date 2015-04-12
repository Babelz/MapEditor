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
        private readonly List<Tileset> tilesets;
        #endregion

        #region Properties
        public IEnumerable<Tileset> Tilesets
        {
            get
            {
                return tilesets;
            }
        }
        #endregion

        public TilesetManager()
        {
            tilesets = new List<Tileset>();
        }

        #region Event handlers
        /// <summary>
        /// Tileset was disposed, remove it from the manager.
        /// </summary>
        private void tileset_Disposing(object sender, EventArgs e)
        {
            Tileset tileset = sender as Tileset;

            tilesets.Remove(tileset);

            tileset.Disposing -= tileset_Disposing;
        }
        #endregion

        /// <summary>
        /// Adds new tileset.
        /// </summary>
        public void AddTileset(Tileset tileset)
        {
            tilesets.Add(tileset);

            tileset.Disposing += tileset_Disposing;
        }

        /// <summary>
        /// Removes and disposes given tileset.
        /// </summary>
        public void RemoveTileset(Tileset tileset)
        {
            tileset.Dispose();
        }

        ~TilesetManager()
        {
            for (int i = 0; i < tilesets.Count; i++) tilesets[i].Dispose();
        }
    }
}

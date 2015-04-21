using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.TileEditor
{
    public sealed class TilesetEventArgs : EventArgs
    {
        #region Fields
        private readonly Tileset tileset;
        #endregion

        #region Properties
        public Tileset Tileset
        {
            get
            {
                return tileset;
            }
        }
        #endregion

        public TilesetEventArgs(Tileset tileset)
        {
            this.tileset = tileset;
        }
    }
}

using MapEditorCore.TileEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.UserControls
{
    public sealed class TilesetEventArgs : EventArgs
    {
        #region Static fields
        public static readonly TilesetEventArgs Empty;
        #endregion

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

        static TilesetEventArgs()
        {
            Empty = new TilesetEventArgs(null);
        }

        public TilesetEventArgs(Tileset tileset)
        {
            this.tileset = tileset;
        }
    }
}

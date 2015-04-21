using MapEditorCore.TileEditor;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorViewModels
{
    public sealed class TilesetViewModel
    {
        #region Fields
        private readonly Tileset tileset;
        #endregion

        #region Properties
        public string Name
        {
            get
            {
                return tileset.Name;
            }
        }
        public string Size
        {
            get
            {
                return string.Format("{0}x{1}pxl", tileset.Texture.Width, tileset.Texture.Height);
            }
        }
        public string TileSize
        {
            get
            {
                return string.Format("{0}x{1}pxl", tileset.SourceSize.X, tileset.SourceSize.Y);
            }
        }
        public int TileWidth
        {
            get
            {
                return tileset.SourceSize.X;
            }
        }
        public int TileHeight
        {
            get
            {
                return tileset.SourceSize.Y;
            }
        }
        public int Rows
        {
            get
            {
                return tileset.IndicesCount.Y;
            }
        }
        public int Columns
        {
            get
            {
                return tileset.IndicesCount.X;
            }
        }
        #endregion

        public TilesetViewModel(Tileset tileset)
        {
            this.tileset = tileset;
        }

        public bool WrapsTileset(Tileset tileset)
        {
            return ReferenceEquals(tileset, this.tileset);
        }
    }
}

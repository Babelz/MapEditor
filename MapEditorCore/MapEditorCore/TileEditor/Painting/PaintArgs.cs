using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorCore.TileEditor.Painting
{
    public sealed class TexturePaintArgs
    {
        #region Fields
        /// <summary>
        /// Source index.
        /// </summary>
        public Point SourceIndex;

        /// <summary>
        /// Tile sheet that contains texture data.
        /// </summary>
        public TileSheet TileSheet;

        /// <summary>
        /// Color to paint with.
        /// </summary>
        public Color Color;
        #endregion
    }

    /// <summary>
    /// Class that contains information regarding painting of the tiles. 
    /// This class will be pooled and there will be few instances in use
    /// everytime.
    /// </summary>
    public sealed class PaintArgs
    {
        #region Fields
        private readonly TexturePaintArgs texturePaintArgs;
        #endregion

        #region Properties
        public TexturePaintArgs TexturePaintArgs
        {
            get
            {
                return texturePaintArgs;
            }
        }
        #endregion

        public PaintArgs()
        {
            texturePaintArgs = new TexturePaintArgs();
        }
    }
}

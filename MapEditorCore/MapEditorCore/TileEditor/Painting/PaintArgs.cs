using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorCore.TileEditor.Painting
{
    /// <summary>
    /// Type of the paint action.
    /// </summary>
    public enum PaintType
    {
        /// <summary>
        /// Texture is being painted.
        /// </summary>
        Texture,

        /// <summary>
        /// Animation is being painted.
        /// </summary>
        Animation
    }

    public sealed class TexturePaintArgs
    {
        #region Fields
        /// <summary>
        /// Source index.
        /// </summary>
        public Point SourceIndex;

        /// <summary>
        /// Tileset that contains texture data.
        /// </summary>
        public TexturedTileset Tileset;

        /// <summary>
        /// Color to paint with.
        /// </summary>
        public Color Color;
        #endregion
    }

    /// <summary>
    /// Class that contains information regarding painting of the tiles. 
    /// This class will be pooled and there will be few instances in use
    /// every time. Only one property of this class can be in valid state
    /// at all times.
    /// </summary>
    public sealed class PaintArgs
    {
        #region Fields
        private readonly TexturePaintArgs texturePaintArgs;
        #endregion

        #region Properties
        /// <summary>
        /// Current type of the paint. Marks 
        /// the valid property.
        /// </summary>
        public PaintType PaintType
        {
            get
            {
                return paintType;
            }
            set
            {
                paintType = value;
            }
        }
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

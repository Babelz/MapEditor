using MapEditorCore.Abstractions;
using MapEditorCore.TileEditor.Painting;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorCore.TileEditor
{
    /// <summary>
    /// Base class for all tiles. Tiles can be animated or textured.
    /// </summary>
    public abstract class Tile : LayerObject
    {
        #region Fields
        private readonly TileEngine tileEngine;

        private TileSheet currentSheet;

        private Color color;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the color of this tile.
        /// </summary>
        public Color Color
        {
            get
            {
                return color;
            }
        }
        
        protected TileEngine TileEngine
        {
            get
            {
                return tileEngine;
            }
        }
        protected TileSheet CurrentSheet
        {
            get
            {
                return currentSheet;
            }
            set
            {
                currentSheet = value;
            }
        }
        #endregion

        public Tile(TileEngine tileEngine, int x, int y)
            : this(tileEngine)
        {
            X = x;
            Y = y;
        }

        public Tile(TileEngine tileEngine)
            : base()
        {
            this.tileEngine = tileEngine;
        }

        /// <summary>
        /// Paints this tile with given args.
        /// </summary>
        /// <param name="args">args used with painting</param>
        public abstract void Paint(PaintArgs args);
    }
}

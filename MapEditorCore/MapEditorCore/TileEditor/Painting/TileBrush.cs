using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.TileEditor.Painting
{
    /*
     * TODO:
     *      Brushed have 1 class
     *      Can resize
     *      Cant resize
     *      Stamp
     *      Single
     *      Multi
     *      Range
     */
    public enum BrushResizeMode
    {
        NoResize,
        CanResize
    }

    /// <summary>
    /// Tool for used to paint tiles.
    /// </summary>
    public sealed class TileBrush
    {
        #region Fields
        private readonly PaintArgs args;

        private Tileset tileset;

        private Point index;
        private Color color;
        #endregion

        #region Properties
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }
        #endregion

        public TileBrush()
        {
            args = new PaintArgs();

            color = Color.White;
        }

        #region Event handlers
        private void tileset_Disposing(object sender, EventArgs e)
        {
            // Tile set was deleted, clear the brush.
            Clear();
        }
        #endregion

        /// <summary>
        /// Clears the brush.
        /// </summary>
        public void Clear()
        {
            color = Color.White;
            index = new Point(-1, -1);

            tileset = null;
        }

        /// <summary>
        /// Returns true if this brush can be used to paint tiles.
        /// </summary>
        public bool CanPaint()
        {
            return index.X != -1 && tileset != null;
        }

        /// <summary>
        /// Selects given index.
        /// </summary>
        public virtual void SelectIndex(int x, int y)
        {
            index.X = x;
            index.Y = y;
        }

        /// <summary>
        /// Selects given tileset.
        /// </summary>
        public virtual void SelectTileset(Tileset tileset)
        {
            this.tileset = tileset;
        }

        /// <summary>
        /// Returns boolean whether brush has finished 
        /// painting. Some brushes can paint multiple tiles 
        /// during one paint call.
        /// </summary>
        /// <returns></returns>
        public abstract bool FinishedPainting();

        /// <summary>
        /// Paints with this brush.
        /// </summary>
        public abstract PaintArgs Paint();
    }
}

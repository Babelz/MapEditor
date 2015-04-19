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
        /// <summary>
        /// Brush has static size, resizing it will throw an exception.
        /// </summary>
        NoResize,

        /// <summary>
        /// Brush can be resize.
        /// </summary>
        CanResize
    }

    /// <summary>
    /// Tool for used to paint tiles.
    /// </summary>
    public abstract class TileBrush
    {
        #region Fields
        private readonly string name;

        private readonly BrushResizeMode resizeMode;
        
        private Tileset tileset;

        private Color color;
        #endregion

        #region Properties
        /// <summary>
        /// Tileset that this brush is using.
        /// </summary>
        public Tileset Tileset
        {
            get
            {
                return tileset;
            }
        }

        public int Width
        {
            get
            {
                return GetWidth();
            }
            set
            {
                Resize(value, Height);
            }
        }
        public int Height
        {
            get
            {
                return GetHeight();
            }
            set
            {
                Resize(Width, value);
            }
        }
        public BrushResizeMode ResizeMode
        {
            get
            {
                return resizeMode;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
        }
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

        protected TileBrush(string name, BrushResizeMode resizeMode)
        {
            this.name = name;

            this.resizeMode = resizeMode;

            color = Color.White;
        }

        #region Event handlers
        private void tileset_Deleting(object sender, TilesetEventArgs e)
        {
            tileset.Deleting -= tileset_Deleting;

            tileset = null;

            SelectIndex(-1, -1);
        }
        #endregion

        private void Resize(int newWidth, int newHeight)
        {
            switch (resizeMode)
            {
                case BrushResizeMode.NoResize:
                    throw new InvalidOperationException("Brush cant be resized.");
                case BrushResizeMode.CanResize:
                    OnResize(newWidth, newHeight);
                    break;
                default:
                    throw new InvalidOperationException("Invalid resize mode.");
            }
        }

        protected abstract int GetWidth();
        protected abstract int GetHeight();

        protected abstract void OnSelect(int x, int y);
        protected virtual void OnResize(int newWidth, int newHeight)
        {
            // Not all brushes can be resized.
        }

        public abstract PaintArgs Paint();
        public abstract bool CanPaint();

        public void SelectTileSheet(Tileset tileset)
        {
            if (this.tileset != null) this.tileset.Deleting -= tileset_Deleting;

            this.tileset = tileset;
            this.tileset.Deleting += tileset_Deleting;
        }

        /// <summary>
        /// Selects given index at given location.
        /// </summary>
        /// <param name="mousePositionX">mouse position x</param>
        /// <param name="mousePositionY">mouse position y</param>
        public void SelectIndex(int mousePositionX, int mousePositionY)
        {
            // Translate to index.
            mousePositionX = mousePositionX / tileset.SourceSize.X;
            mousePositionY = mousePositionY / tileset.SourceSize.Y;

            // Validate that the index is in bounds.
            int rows = tileset.IndicesCount.Y;
            int columns = tileset.IndicesCount.X;

            int width = GetWidth() - 1;
            int height = GetHeight() - 1;

            mousePositionX = mousePositionX + width > columns ? columns - mousePositionX - width : mousePositionX;
            mousePositionY = mousePositionY + height > rows ? rows - mousePositionY - height : mousePositionY;

            OnSelect(mousePositionX, mousePositionY);
        }
    }
}

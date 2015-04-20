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
    /// TODO: refactor this class?
    /// </summary>
    public abstract class TileBrush
    {
        #region Fields
        private readonly string name;

        private readonly BrushResizeMode resizeMode;

        private readonly Tileset owner;

        private Color color;
        #endregion

        #region Properties
        public Tileset Owner
        {
            get
            {
                return owner;
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

        public TileBrush(string name, BrushResizeMode resizeMode, Tileset owner)
        {
            this.name = name;
            this.resizeMode = resizeMode;
            this.owner = owner;

            color = Color.White;
        }

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

        protected virtual void OnResize(int newWidth, int newHeight)
        {
            // Not all brushes can be resized.
        }

        public abstract PaintArgs Paint();
        public abstract bool CanPaint();

        /// <summary>
        /// Selects given index at given location.
        /// </summary>
        /// <param name="x">mouse position x</param>
        /// <param name="y">mouse position y</param>
        public abstract void SelectIndex(int x, int y);
    }
}

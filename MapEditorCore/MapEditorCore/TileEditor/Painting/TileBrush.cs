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

        private Point selectedIndex;
        private Color color;

        private bool painting;
        #endregion

        #region Properties
        public string Name
        {
            get
            {
                return name;
            }
        }
        
        public Tileset Owner
        {
            get
            {
                return owner;
            }
        }

        /// <summary>
        /// Returns resize mode of the brush.
        /// </summary>
        public BrushResizeMode ResizeMode
        {
            get
            {
                return resizeMode;
            }
        }
        /// <summary>
        /// Gets of sets the color of the brush.
        /// </summary>
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

        public int SelectedIndexX
        {
            get
            {
                return selectedIndex.X;
            }
        }
        public int SelectedIndexY
        {
            get
            {
                return selectedIndex.Y;
            }
        }

        /// <summary>
        /// Returns width of the brush.
        /// </summary>
        public abstract int Width
        {
            get;
        }
        /// <summary>
        /// Returns height of the brush.
        /// </summary>
        public abstract int Height
        {
            get;
        }

        /// <summary>
        /// Returns display width of the brush.
        /// </summary>
        public abstract int DisplayWidth
        {
            get;
        }
        /// <summary>
        /// Returns display height of the brush.
        /// </summary>
        public abstract int DisplayHeight
        {
            get;
        }
        #endregion

        public TileBrush(string name, BrushResizeMode resizeMode, Tileset owner)
        {
            this.name = name;
            this.resizeMode = resizeMode;
            this.owner = owner;

            color = Color.White;
        }

        protected virtual void OnSelectedIndexChanged(int newX, int newY)
        {
        }
        protected virtual void OnResize(int newWidth, int newHeight)
        {
            // Not all brushes can be resized.
        }

        /// <summary>
        /// Called in paint.
        /// </summary>
        /// <returns></returns>
        protected abstract PaintArgs OnPaint();
        

        /// <summary>
        /// Finishes drawing.
        /// </summary>
        protected void FinishPainting()
        {
            painting = false;
        }

        public void Resize(int newWidth, int newHeight)
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

        /// <summary>
        /// Returns true if brush can be used to paint.
        /// </summary>
        /// <returns></returns>
        public bool CanPaint()
        {
            return SelectedIndexX != -1;
        }

        /// <summary>
        /// Returns true if brush is painting.
        /// </summary>
        public bool Painting()
        {
            return painting;
        }

        /// <summary>
        /// Should be called when painting starts.
        /// </summary>
        public void BeginPainting()
        {
            painting = true;
        }

        /// <summary>
        /// Returns next paint args of the brush.
        /// </summary>
        /// <returns>next args to be used with painting</returns>
        public PaintArgs Paint()
        {
            if (!painting) throw new InvalidOperationException("Begin painting must be called before Paint.");

            return OnPaint();
        }

        /// <summary>
        /// Should be called when painting ends.
        /// </summary>
        public void EndPainting()
        {
            painting = false;
        }

        /// <summary>
        /// Selects given index at given location.
        /// </summary>
        /// <param name="x">mouse position x</param>
        /// <param name="y">mouse position y</param>
        public void SelectIndex(int x, int y)
        {
            selectedIndex.X = x;
            selectedIndex.Y = y;

            OnSelectedIndexChanged(x, y);
        }
    }
}

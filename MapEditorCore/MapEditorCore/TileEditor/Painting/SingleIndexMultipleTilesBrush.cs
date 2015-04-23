using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.TileEditor.Painting
{
    public sealed class SingleIndexMultipleTilesBrush : TileBrush
    {
        #region Fields
        private const int DISPLAY_WIDTH = 1;
        private const int DISPLAY_HEIGHT = 1;
        
        private readonly PaintArgs args;

        private int width;
        private int height;

        private int argsCount;
        #endregion

        #region Properties
        public override int Width
        {
            get
            {
                return width;
            }
        }
        public override int Height
        {
            get
            {
                return height;
            }
        }
        public override int DisplayWidth
        {
            get
            {
                return DISPLAY_WIDTH;
            }
        }
        public override int DisplayHeight
        {
            get
            {
                return DISPLAY_HEIGHT;
            }
        }
        #endregion

        public SingleIndexMultipleTilesBrush(int width, int height, BrushResizeMode resizeMode, string name, Tileset owner)
            : base(name, resizeMode, owner)
        {
            this.width = width;
            this.height = height;

            args = new PaintArgs();
            args.PaintType = PaintType.Texture;
            args.TexturePaintArgs.Tileset = owner;
        }

        protected override void OnResize(int newWidth, int newHeight)
        {
            width = newWidth;
            height = newHeight;
        }

        /*
         * Display is always 1, internal height and width can be different.
         */

        protected override void OnSelectedIndexChanged(int newX, int newY)
        {
            args.TexturePaintArgs.SourceIndex.X = SelectedIndexX;
            args.TexturePaintArgs.SourceIndex.Y = SelectedIndexY;
        }

        protected override PaintArgs OnPaint()
        {
            argsCount++;

            if (argsCount >= width * height)
            {
                // Finished painting.
                argsCount = 0;

                FinishPainting();
            } 

            return args;
        }
    }
}

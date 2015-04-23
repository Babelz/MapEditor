using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.TileEditor.Painting
{
    public sealed class MultipleIndicesMultipleTilesBrush : TileBrush
    {
        #region Fields
        private PaintArgs[] paintArgs;

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
                return width;
            }
        }
        public override int DisplayHeight
        {
            get
            {
                return height;
            }
        }
        #endregion

        public MultipleIndicesMultipleTilesBrush(int width, int height, BrushResizeMode resizeMode, string name, Tileset owner)
            : base(name, resizeMode, owner)
        {
            this.width = width;
            this.height = height;

            UpdateArgs();
        }

        private PaintArgs CreateNewArgs()
        {
            PaintArgs args = new PaintArgs();
            
            args.PaintType = PaintType.Texture;
            args.TexturePaintArgs.Tileset = Owner;

            return args;
        }

        private void UpdateArgs()
        {
            // Initial init.
            if (paintArgs == null)
            {
                paintArgs = new PaintArgs[width * height];

                // Initialize args.
                for (int i = 0; i < paintArgs.Length; i++) paintArgs[i] = CreateNewArgs();

                // Return after initial init.
                return;
            }
            
            // Resize.
            int oldSize = paintArgs.Length;
            int newSize = width * height;

            Array.Resize(ref paintArgs, newSize);

            // Array should not contains any nulls, just return.
            if (newSize < oldSize) return;
            
            // Initialize nulls.
            for (int i = oldSize; i < newSize; i++) paintArgs[i] = CreateNewArgs(); 
        }

        protected override void OnResize(int newWidth, int newHeight)
        {
            width = newWidth;
            height = newHeight;

            UpdateArgs();
        }

        protected override void OnSelectedIndexChanged(int newX, int newY)
        {
            // Update source indices of all args.
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int k = i * height + j;

                    PaintArgs args = paintArgs[k];

                    args.TexturePaintArgs.SourceIndex.X = newX + j;
                    args.TexturePaintArgs.SourceIndex.Y = newY + i;
                }
            }
        }

        protected override PaintArgs OnPaint()
        {
            // Get next arg and return it.
            PaintArgs args = paintArgs[argsCount];
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

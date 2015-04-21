using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.TileEditor.Painting
{
    public sealed class SingleTileBrush : TileBrush
    {
        #region Fields
        private const int WIDTH = 1;
        private const int HEIGHT = 1;

        private readonly PaintArgs args;
        #endregion

        #region Properties
        public override int Width
        {
            get
            {
                return WIDTH;
            }
        }
        public override int Height
        {
            get
            {
                return HEIGHT;
            }
        }
        public override int DisplayWidth
        {
            get
            {
                return WIDTH;
            }
        }

        public override int DisplayHeight
        {
            get
            {
                return HEIGHT;
            }
        }
        #endregion

        public SingleTileBrush(Tileset owner)
            : base("Single", BrushResizeMode.NoResize, owner)
        {
            // Initialize non changing data of the args.
            // TODO: args could have immutable members.
            args = new PaintArgs();
            args.PaintType = PaintType.Texture;
            args.TexturePaintArgs.Tileset = Owner;
        }

        protected override PaintArgs OnPaint()
        {
            args.TexturePaintArgs.Color = Color;
            args.TexturePaintArgs.SourceIndex.X = SelectedIndexX;
            args.TexturePaintArgs.SourceIndex.Y = SelectedIndexY;

            FinishPainting();

            return args;
        }
    }
}

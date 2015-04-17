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

        private Point selectedIndex;
        #endregion

        public SingleTileBrush()
            : base("Single", BrushResizeMode.NoResize)
        {
            args = new PaintArgs();
        }

        protected override int GetWidth()
        {
            return WIDTH;
        }
        protected override int GetHeight()
        {
            return HEIGHT;
        }

        protected override void OnSelect(int x, int y)
        {
            selectedIndex.X = x;
            selectedIndex.Y = y;
        }

        public override PaintArgs Paint(int x, int y)
        {
            args.PaintType = PaintType.Texture;

            args.TexturePaintArgs.Color = Color;
            args.TexturePaintArgs.SourceIndex = selectedIndex;

            return args;
        }

        public override bool CanPaint()
        {
            return selectedIndex.X != -1;
        }
    }
}

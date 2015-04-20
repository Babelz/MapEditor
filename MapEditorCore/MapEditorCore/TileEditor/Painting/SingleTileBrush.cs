﻿using Microsoft.Xna.Framework;
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

        private bool painted;
        #endregion

        public SingleTileBrush(Tileset owner)
            : base("Single", BrushResizeMode.NoResize, owner)
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

        public override void SelectIndex(int x, int y)
        {
            selectedIndex.X = x;
            selectedIndex.Y = y;
        }

        public override PaintArgs Paint()
        {
            // Set state to true.
            painted = true;

            args.PaintType = PaintType.Texture;
            args.TexturePaintArgs.Tileset = Owner;

            args.TexturePaintArgs.Color = Color;
            args.TexturePaintArgs.SourceIndex = selectedIndex;

            return args;
        }

        public override bool CanPaint()
        {
            // Single paint, swap state.
            if (!painted && selectedIndex.X != -1)
            {
                painted = true;

                return painted;
            }

            painted = false;

            return painted;
        }
    }
}

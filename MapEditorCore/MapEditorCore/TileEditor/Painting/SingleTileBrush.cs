using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.TileEditor.Painting
{
    public sealed class SingleTileBrush : TileBrush
    {
        public SingleTileBrush()
        {
        }

        public override bool FinishedPainting()
        {
            // Just return true since this brush paints once.
            return true;
        }

        public override PaintArgs Paint()
        {
            // Set paint argument values.
            Args.PaintType = PaintType.Texture;

            Args.TexturePaintArgs.SourceIndex.X = Index.X;
            Args.TexturePaintArgs.SourceIndex.Y = Index.Y;
            Args.TexturePaintArgs.Color = Color;

            return Args;
        }
    }
}

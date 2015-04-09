using MapEditorCore.TileEditor.Painting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorCore.TileEditor
{
    public sealed class TexturedTile : Tile
    {
        #region Fields
        private Point sourceIndex;

        private Rectangle destination;
        private Rectangle source;
        #endregion

        public TexturedTile(TileEngine tileEngine, int x, int y)
            : base(tileEngine, x, y)
        {
            sourceIndex = new Point(-1, -1);
        }
        public TexturedTile(TileEngine tileEngine)
            : base(tileEngine)
        {
            sourceIndex = new Point(-1, -1);
        }

        protected override void OnMoved(Point oldPosition, Point newPosition)
        {
            destination = new Rectangle(newPosition.X, newPosition.Y, TileEngine.TileBounds.Width, TileEngine.TileBounds.Height);
        }

        #region Event handlers
        private void CurrentSheet_Disposing(object sender, EventArgs e)
        {
            // Reset tile. Sheet has been disposed.
            CurrentSheet.Disposing -= CurrentSheet_Disposing;
            CurrentSheet = null;

            Clear();
        }
        #endregion

        private void Clear()
        {
            sourceIndex = new Point(-1, -1);
        }

        protected override void OnDraw(SpriteBatch spriteBatch)
        {
            // If tile has source, draw it. No need to check for
            // null sheets because there cant be invalid indices 
            // if tile has sheet.
            if (sourceIndex.X != -1)
            {
                spriteBatch.Draw(CurrentSheet.Texture, destination, source, Color);
            }
        }

        public override void Paint(PaintArgs args)
        {
            // Swap sheet if needed.
            if(CurrentSheet != null)
                CurrentSheet.Disposing -= CurrentSheet_Disposing;

            CurrentSheet = args.TexturePaintArgs.TileSheet;

            // Hook new event if args contain new sheet.
            if(ReferenceEquals(CurrentSheet, args.TexturePaintArgs.TileSheet))
                CurrentSheet.Disposing += CurrentSheet_Disposing;

            // Get new index and source.
            sourceIndex = args.TexturePaintArgs.SourceIndex;
            source = CurrentSheet.GetSource(sourceIndex);
        }
    }
}

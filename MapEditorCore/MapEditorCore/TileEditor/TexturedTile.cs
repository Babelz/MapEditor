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
        private void CurrentTileset_Disposing(object sender, EventArgs e)
        {
            // Reset tile. Set has been disposed.
            CurrentTileset.Disposing -= CurrentTileset_Disposing;
            CurrentTileset = null;

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
            // null set because there cant be invalid indices 
            // if tile has set.
            if (sourceIndex.X != -1)
            {
                spriteBatch.Draw(CurrentTileset.Texture, destination, source, Color);
            }
        }

        public override void Paint(PaintArgs args)
        {
            // Swap set if needed.
            if(CurrentTileset != null)
                CurrentTileset.Disposing -= CurrentTileset_Disposing;

            CurrentTileset = args.TexturePaintArgs.Tileset;

            // Hook new event if args contain new set.
            if(ReferenceEquals(CurrentTileset, args.TexturePaintArgs.Tileset))
                CurrentTileset.Disposing += CurrentTileset_Disposing;

            // Get new index and source.
            sourceIndex = args.TexturePaintArgs.SourceIndex;
            source = CurrentTileset.GetSource(sourceIndex);
        }
    }
}

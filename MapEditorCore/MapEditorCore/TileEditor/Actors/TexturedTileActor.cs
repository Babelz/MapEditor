﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.TileEditor.Actors
{
    public sealed class TexturedTileActor : TileActor
    {
        #region Fields
        private TexturedTileset tileset;

        private Point sourceIndex;

        private Rectangle destination;
        private Rectangle source;
        #endregion

        public TexturedTileActor(TileEngine tileEngine, Tile tile)
            : base(tileEngine, tile)
        {
            sourceIndex = new Point(-1, -1);

            // Do same thing at moved event handler. Just copy it to avoid call overhead.
            destination = new Rectangle(X, Y, TileEngine.TileBounds.Width, TileEngine.TileBounds.Height);

            tile.Moved += tile_Moved;
        }

        #region Event handlers
        /// <summary>
        /// Tile was moved, recalculate destination rectangle.
        /// </summary>
        private void tile_Moved(Point oldPosition, Point newPosition)
        {
            destination = new Rectangle(newPosition.X, newPosition.Y, TileEngine.TileBounds.Width, TileEngine.TileBounds.Height);
        }
        /// <summary>
        /// Tileset was disposed. Reset the actor.
        /// </summary>
        private void tileset_Disposing(object sender, EventArgs e)
        {
            // Remove event.
            tileset.Disposing -= tileset_Disposing;
            tileset = null;
            
            Clear();
        }
        #endregion

        public override void Clear()
        {
            sourceIndex = new Point(-1, -1);
        }

        public override void Paint(Painting.PaintArgs args)
        {
            // Swap set if needed.
            if (!ReferenceEquals(tileset, args.TexturePaintArgs.Tileset))
            {
                if(tileset != null) tileset.Disposing -= tileset_Disposing;

                // Swap set.
                tileset = args.TexturePaintArgs.Tileset;
                
                // Null swap. Clear and return.
                if (tileset == null) 
                {
                    Clear();

                    return;
                }
            }

            // Got sheet.
            sourceIndex = args.TexturePaintArgs.SourceIndex;

            // TODO: paint the color.
            if (sourceIndex.X != -1) source = tileset.GetSource(sourceIndex);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (sourceIndex.X != -1) spriteBatch.Draw(tileset.Texture, destination, source, Color);
        }
    }
}
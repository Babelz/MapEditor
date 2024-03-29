﻿using MapEditorCore.Abstractions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorCore.TileEditor
{
    public sealed class TileLayer : Layer
    {
        #region Fields
        private readonly TileEngine tileEngine;

        private Tile[][] tiles;

        // Values used to find tiles that are inside the view.
        private int fromRow;
        private int fromColumn;

        private int toRow;
        private int toColumn;
        #endregion

        public TileLayer(string name, Point size, TileEngine tileEngine)
            : base(name, size)
        {
            this.tileEngine = tileEngine;

            // Initialize tiles.
            tiles = new Tile[Height][];

            for (int i = 0; i < tiles.Length; i++) tiles[i] = new Tile[Width];

            InitializeTileArray(ref tiles);
        }

        private void CalculateTileRange(Rectangle viewBounds) 
        {
            // Padding of 5 tiles.
            // TODO: const padding, wont work with zoom.
            const int padding = 5;

            // Calculate index values.
            int leftIndex = viewBounds.Left / tileEngine.TileSizeInPixels.X;
            int rightIndex = viewBounds.Right / tileEngine.TileSizeInPixels.X;
            int topIndex = viewBounds.Top / tileEngine.TileSizeInPixels.Y;
            int bototmIndex = viewBounds.Bottom / tileEngine.TileSizeInPixels.Y;

            // Validate that "from" values are in bounds.
            fromRow = leftIndex - padding; 
            fromRow = fromRow < 0 ? 0 : fromRow;

            fromColumn = topIndex - padding; 
            fromColumn = fromColumn < 0 ? 0 : fromColumn;

            // Validate that "to" values are in bounds.
            toRow = rightIndex + padding; 
            toRow = toRow > Height ? Height : toRow;
            
            toColumn = bototmIndex + padding; 
            toColumn = toColumn > Width ? Width : toColumn;
        }
        private void RepositionTiles()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    tiles[i][j].X = j * tileEngine.TileBounds.Width + X;
                    tiles[i][j].Y = i * tileEngine.TileBounds.Height + Y;
                }
            }
        }
        private void InitializeTileArray(ref Tile[][] tiles) 
        {
            // TODO: initialize from range?
            //       could get slow with bigger resize
            //       operations.

            for (int i = fromRow; i < tiles.Length; i++)
            {
                for (int j = fromColumn; j < tiles[i].Length; j++)
                {
                    if (tiles[i][j] != null) continue;
 
                    tiles[i][j] = new Tile(tileEngine, j * tileEngine.TileSizeInPixels.X + X, 
                                                       i * tileEngine.TileSizeInPixels.Y + Y);
                }
            }
        }

        protected override void MakeStatic()
        {
            // TODO: make tile layer static.
        }

        protected override void MakeDynamic()
        {
            // TODO: make tile layer dynamic.
        }

        protected override void OnUpdate(GameTime gameTime, Rectangle viewBounds)
        {
            CalculateTileRange(viewBounds);

            for (int i = fromRow; i < toRow; i++)
            {
                for (int j = fromColumn; j < toColumn; j++)
                {
                    tiles[i][j].Update(gameTime);
                }
            }
        }

        protected override void OnDraw(SpriteBatch spriteBatch, Rectangle viewBounds)
        {
            // No need to recalculate range because it was done inside update.
            // Just draw the layer. 
            for (int i = fromRow; i < toRow; i++)
            {
                for (int j = fromColumn; j < toColumn; j++)
                {
                    tiles[i][j].Draw(spriteBatch);
                }
            }
        }

        public override void Resize(Point newSize)
        {
            // No need to validate size. Should always be correct.
            Array.Resize(ref tiles, newSize.Y);

            for (int i = 0; i < tiles.Length; i++) Array.Resize(ref tiles[i], newSize.X);

            // Initialize empty columns.
            InitializeTileArray(ref tiles);

            // TODO: if layer is static, it should redraw itself.
            
            base.Resize(newSize);
        }

        public override void MoveTo(Point newPosition)
        {
            base.MoveTo(newPosition);

            RepositionTiles();
        }

        public override void MoveBy(Point amount)
        {
            base.MoveBy(amount);

            RepositionTiles();
        }

        public Tile TileAtIndex(int x, int y)
        {
            return tiles[y][x];
        }
    }
}

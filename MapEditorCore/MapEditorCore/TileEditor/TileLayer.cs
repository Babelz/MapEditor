using MapEditorCore.Abstractions;
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
        #endregion

        public TileLayer(string name, Point size, TileEngine tileEngine)
            : base(name, size)
        {
            this.tileEngine = tileEngine;

            // Initialize tiles.
            tiles = new Tile[Height][];

            for (int i = 0; i < tiles.Length; i++) tiles[i] = new Tile[Width];

            InitializeTileArray(ref tiles, 0, 0);
        }

        private void CalculateArrayRange(Rectangle viewBounds, ref int fromRow, ref int fromColumn, ref int toRow, ref int toColumn) 
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

            // Validate thath "to" values are in bounds.
            toRow = rightIndex + padding; 
            toRow = toRow > tileEngine.MaxLayerSizeInTiles.Y ? tileEngine.MaxLayerSizeInTiles.X : toRow;
            
            toColumn = leftIndex + padding; 
            toColumn = toColumn > tileEngine.MaxLayerSizeInTiles.X ? tileEngine.MaxLayerSizeInTiles.X : toColumn;
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
        private void InitializeTileArray(ref Tile[][] tiles, int fromRow, int fromColumn) 
        {
            for (int i = fromRow; i < tiles.Length; i++)
            {
                for (int j = fromColumn; j < tiles[i].Length; j++)
                {
                    tiles[i][j] = new Tile(tileEngine, j * tileEngine.TileBounds.Width + X, 
                                                       i * tileEngine.TileBounds.Height + Y);
                }
            }
        }
        /// <summary>
        /// Executes action for each tile in given bounds.
        /// </summary>
        /// <param name="viewBounds">bounds</param>
        /// <param name="action">action to execute</param>
        private void ForTilesInrange(Rectangle viewBounds, Action<Tile> action)
        {
            int fromRow = 0;
            int fromColumn = 0;
            int toRow = 0;
            int toColumn = 0;

            CalculateArrayRange(viewBounds, ref fromRow, ref fromColumn, ref toRow, ref toColumn);

            for (int i = fromRow; i < toRow; i++)
            {
                for (int j = fromColumn; j < toColumn; j++)
                {
                    action(tiles[i][j]);
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
            ForTilesInrange(viewBounds, t => t.Update(gameTime));
        }

        protected override void OnDraw(SpriteBatch spriteBatch, Rectangle viewBounds)
        {
            ForTilesInrange(viewBounds, t => t.Draw(spriteBatch));
        }

        protected override void Resize(Point newSize)
        {
            // No need to validate size. Should always be correct.
            Array.Resize(ref tiles, newSize.Y);

            for (int i = 0; i < tiles.Length; i++) Array.Resize(ref tiles[i], newSize.X);

            int fromColumn = Width < newSize.X ? Width : newSize.X;
            int fromRow = Height < newSize.Y ? Height : newSize.Y;

            InitializeTileArray(ref tiles, fromRow, fromColumn);
            
            base.Resize(newSize);
        }

        protected override void MoveTo(Point newPosition)
        {
            base.MoveTo(newPosition);

            RepositionTiles();
        }

        protected override void MoveBy(Point amount)
        {
            base.MoveBy(amount);

            RepositionTiles();
        }
    }
}

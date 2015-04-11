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

        protected override void MakeStatic()
        {
            // TODO: make tile layer static.
        }

        protected override void MakeDynamic()
        {
            // TODO: make tile layer dynamic.
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    tiles[i][j].Update(gameTime);
                }
            }
        }

        protected override void OnDraw(SpriteBatch spriteBatch, Rectangle viewBounds)
        {
            // TODO: calc render area.
            int fromRow = 0;
            int fromColumn = 0;

            int toRow = Height;
            int toColumn = Width;

            for (int i = fromRow; i < toRow; i++)
            {
                for (int j = fromColumn; j < toColumn; j++)
                {
                    tiles[i][j].Draw(spriteBatch);
                }
            }
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

using MapEditorCore.Abstractions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.TileEditor
{
    public sealed class TileEngine
    {
        #region Fields
        private readonly Point tileSizeInPixels;
        
        private Rectangle pixelBounds;
        private Rectangle tileBounds;

        private Point maxLayerSizeInTiles;
        #endregion

        #region Properites
        /// <summary>
        /// Engine bounds in pixels.
        /// </summary>
        public Rectangle PixelBounds
        {
            get
            {
                return pixelBounds;
            }
        }
        /// <summary>
        /// Engine bounds in tiles.
        /// </summary>
        public Rectangle TileBounds
        {
            get
            {
                return tileBounds;
            }
        }
        /// <summary>
        /// Returns tile size in pixels.
        /// </summary>
        public Point TileSizeInPixels
        {
            get
            {
                return tileSizeInPixels;
            }
        }
        /// <summary>
        /// Gets or sets max layer size in tiles.
        /// </summary>
        public Point MaxLayerSizeInTiles
        {
            get
            {
                return maxLayerSizeInTiles;
            }
            set
            {
                Point oldSize = maxLayerSizeInTiles;
                maxLayerSizeInTiles = value;

                if (oldSize != maxLayerSizeInTiles)
                    CalculateBounds();
            }
        }
        #endregion

        public TileEngine(Point maxLayerSizeInTiles, Point tileSizeInPixels)
        {
            this.maxLayerSizeInTiles = maxLayerSizeInTiles;
            this.tileSizeInPixels = tileSizeInPixels;

            CalculateBounds();
        }

        /// <summary>
        /// Calculate bounds of the engine.
        /// </summary>
        private void CalculateBounds()
        {
            tileBounds = new Rectangle(0, 0, maxLayerSizeInTiles.X, maxLayerSizeInTiles.Y);

            pixelBounds = new Rectangle(0, 0, tileBounds.Width * tileSizeInPixels.X,
                                              tileBounds.Height * tileSizeInPixels.Y);
        }
    }
}

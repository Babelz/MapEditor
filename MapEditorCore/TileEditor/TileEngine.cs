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
        private readonly IView view;

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
        /// <summary>
        /// View that this engine is using.
        /// </summary>
        public IView View
        {
            get
            {
                return view;
            }
        }
        #endregion

        public TileEngine(IView view, Point maxLayerSizeInTiles, Point tileSizeInPixels)
        {
            this.view = view;
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

            pixelBounds = new Rectangle(0, 0, tileBounds.X * tileSizeInPixels.X,
                                              tileBounds.Y * tileSizeInPixels.Y);
        }
    }
}

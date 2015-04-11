using MapEditorCore.TileEditor.Painting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.TileEditor.Actors
{
    /// <summary>
    /// Base class for all tile actors. Actor defines
    /// functionality of a tile. It handles rendering, updating and 
    /// managing the tileset of the tile. Tile is just and adapter
    /// and decides wich actor to use. Selection is based upon pain args.
    /// </summary>
    public abstract class TileActor
    {
        #region Fields
        private readonly TileEngine tileEngine;

        private readonly Tile tile;

        private Color color;
        #endregion

        #region Properties
        protected Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }
        protected Tile Tile
        {
            get
            {
                return tile;
            }
        }
        protected TileEngine TileEngine
        {
            get
            {
                return tileEngine;
            }
        }
        #endregion

        public TileActor(TileEngine tileEngine, Tile tile)
        {
            this.tileEngine = tileEngine;
            this.tile = tile;
        }

        /// <summary>
        /// Set the state of the actor to 
        /// start state.
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// Called when tile is painted.
        /// </summary>
        public abstract void Paint(PaintArgs args);
        
        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}

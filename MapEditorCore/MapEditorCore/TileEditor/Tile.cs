using MapEditorCore.Abstractions;
using MapEditorCore.TileEditor.Actors;
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
    /// <summary>
    /// Adapter class for tile actors.
    /// </summary>
    public sealed class Tile : LayerObject
    {
        #region Fields
        // Dictionary of all used actors. 
        private readonly Dictionary<PaintType, TileActor> actors;

        private readonly TileEngine tileEngine;

        private TileActor currentActor;
        private PaintType lastPaintType;
        #endregion

        public Tile(TileEngine tileEngine, int x, int y)
            : this(tileEngine)
        {
            X = x;
            Y = y;
        }

        public Tile(TileEngine tileEngine)
            : base()
        {
            this.tileEngine = tileEngine;

            // Add all supported actors.
            actors = new Dictionary<PaintType, TileActor>()
            {
                { PaintType.Texture, new TexturedTileActor(tileEngine, this) }    
            };
        
            // Set textured tile actor as default.
            lastPaintType = PaintType.Texture;
            currentActor = actors[lastPaintType];
        }

        public void Paint(PaintArgs args)
        {
            // Check if the actor is valid for this paint operation.
            if (lastPaintType != args.PaintType)
            {
                // Invalid actor. Change it.
                currentActor.Clear();

                currentActor = actors[args.PaintType];
            }

            lastPaintType = args.PaintType;

            // Let the actor paint.
            currentActor.Paint(args);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            currentActor.Update(gameTime);
        }
        public override void OnDraw(SpriteBatch spriteBatch)
        {
            currentActor.Draw(spriteBatch);
        }
    }
}

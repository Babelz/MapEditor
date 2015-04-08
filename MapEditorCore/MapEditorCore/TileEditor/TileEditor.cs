using MapEditorCore.Abstractions;
using MapEditorCore.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorCore.TileEditor
{
    public sealed class TileEditor : Editor
    {
        #region Fields
        private readonly LayerManager<TileLayer> layers;

        private readonly TileEngine tileEngine;
        private readonly BasicView view;

        private Color backgroundColor;
        #endregion

        #region Properties
        public override IEnumerable<Layer> Layers
        {
            get
            {
                return layers.Layers;
            }
        }
        public override Color BackgroundColor
        {
            get
            {
                return backgroundColor;
            }
            set
            {
                backgroundColor = value;
            }
        }
        #endregion

        public TileEditor(TileEngine tileEngine)
            : base()
        {
            this.tileEngine = tileEngine;

            layers = new LayerManager<TileLayer>();
            view = new BasicView();

            backgroundColor = Color.CornflowerBlue;
        }

        protected override void OnInitialize()
        {
            // TODO: initialize components.
        }

        public override void MakeLayerActive(string name)
        {
            layers.MakeActive(name);
        }

        public override void AddLayer(string name, Point size)
        {
            TileLayer layer = new TileLayer(name, size, tileEngine);

            layers.AddLayer(layer);
        }
        public override void RemoveLayer(string name)
        {
            layers.RemoveLayer(name);
        }

        public override Rectangle GetMapBounds()
        {
            return tileEngine.PixelBounds;
        }

        public override void Update(GameTime gameTime)
        {
            layers.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            layers.Draw(spriteBatch, view.Bounds);

            spriteBatch.End();
        }
    }
}

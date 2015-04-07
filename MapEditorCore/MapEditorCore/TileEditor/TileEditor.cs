using MapEditorCore.Abstractions;
using MapEditorCore.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorCore.TileEditor
{
    public sealed class TileEditor : IEditor
    {
        #region Fields
        private readonly LayerManager<TileLayer> layers;

        private readonly TileEngine tileEngine;
        private readonly BasicView view;

        private Color backgroundColor;
        #endregion

        #region Properties
        public IEnumerable<Layer> Layers
        {
            get
            {
                return layers.Layers;
            }
        }
        public Color BackgroundColor
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
        {
            this.tileEngine = tileEngine;

            layers = new LayerManager<TileLayer>();
            view = new BasicView();

            backgroundColor = Color.CornflowerBlue;
        }

        public void Initialize()
        {
        }

        public void LoadContent()
        {
        }

        public void UnloadContent()
        {
        }

        public void MakeLayerActive(string name)
        {
            layers.MakeActive(name);
        }

        public void AddLayer(string name, Point size)
        {
            TileLayer layer = new TileLayer(name, size, tileEngine);

            layers.AddLayer(layer);
        }

        public void RemoveLayer(string name)
        {
            layers.RemoveLayer(name);
        }

        public void Update(GameTime gameTime)
        {
            layers.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            layers.Draw(spriteBatch, view.Bounds);

            spriteBatch.End();
        }
    }
}

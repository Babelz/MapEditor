using MapEditorCore.Abstractions;
using MapEditorCore.Components;
using MapEditorCore.Components.EditorComponents;
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
        private readonly ComponentCollection components;
        private readonly List<Tileset> tilesets;

        private readonly TileEngine tileEngine;
        private readonly BasicView view;

        private Color backgroundColor;
        #endregion

        #region Properties
        public IEnumerable<Tileset> Tilesets
        {
            get
            {
                return tilesets;
            }
        }
        public override IEnumerable<Layer> Layers
        {
            get
            {
                return layers.Layers;
            }
        }
        public override IEnumerable<EditorComponent> Components
        {
            get
            {
                return components.Components;
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
        public TileEngine TileEngine
        {
            get
            {
                return tileEngine;
            }
        }
        #endregion

        public TileEditor(TileEngine tileEngine)
            : base()
        {
            this.tileEngine = tileEngine;

            layers = new LayerManager<TileLayer>();
            components = new ComponentCollection();
            tilesets = new List<Tileset>();

            view = new BasicView();

            backgroundColor = Color.CornflowerBlue;
        }

        #region Event handlers
        private void tileset_Disposing(object sender, EventArgs e)
        {
            Tileset tileset = sender as Tileset;
            tileset.Disposing -= tileset_Disposing;

            tilesets.Remove(tileset);
        }
        #endregion

        protected override void OnInitialize()
        {
            // 1x1 white texture.
            Texture2D temp = Content.Load<Texture2D>("temp");

            components.AddComponent(new BorderRenderer(this, temp));
            components.AddComponent(new Grid(this, temp, new Point(tileEngine.TileSizeInPixels.X, tileEngine.TileSizeInPixels.Y)));
        }

        public override void SelectLayer(string name)
        {
            layers.SelectLayer(name);
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

        public void SelectTileset(string name)
        {
            // TODO: make tile set manager and wrap methods here.
        }
        public void AddTileset(string name, Texture2D texture, Point sourceSize, Point offset)
        {
            // TODO: make tile set manager and wrap methods here.
        }
        public void RemoveTileset(string name)
        {
            // TODO: make tile set manager and wrap methods here.
        }

        // TODO: add animation metohds.

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

            components.Draw(spriteBatch, view.Bounds);

            spriteBatch.End();
        }
    }
}

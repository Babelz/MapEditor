using MapEditorCore.Abstractions;
using MapEditorCore.Components;
using MapEditorCore.Components.EditorComponents;
using MapEditorCore.Input;
using MapEditorCore.Input.Listener;
using MapEditorCore.Input.Trigger;
using MapEditorCore.TileEditor.Painting;
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
        private readonly ResourceManager<Texture2D> textures;
        private readonly LayerManager<TileLayer> layers;
        private readonly ComponentCollection components;
        private readonly TilesetManager tilesets;

        private readonly TileEngine tileEngine;
        private readonly BasicView view;

        private readonly KeyboardInputListener keyboardInputListener;
        private readonly MouseInputListener mouseInputListener;
        private readonly InputManager inputManager;

        private Color backgroundColor;
        #endregion

        #region Properties
        public IEnumerable<Tileset> Tilesets
        {
            get
            {
                return tilesets.Tilesets;
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

            textures = new ResourceManager<Texture2D>();
            layers = new LayerManager<TileLayer>();
            components = new ComponentCollection();
            tilesets = new TilesetManager();
            
            view = new BasicView();

            keyboardInputListener = new KeyboardInputListener();
            mouseInputListener = new MouseInputListener();
            inputManager = new InputManager(keyboardInputListener, mouseInputListener);

            backgroundColor = Color.CornflowerBlue;
        }

        private void InitializeInput()
        {
            mouseInputListener.Map("interact", (args) => 
            {
            }, new MouseTrigger(MouseButtons.LeftButton));
        }

        protected override void OnInitialize()
        {
            // 1x1 white texture.
            Texture2D temp = Content.Load<Texture2D>("temp");

            components.AddComponent(new BorderRenderer(this, temp));
            components.AddComponent(new Grid(this, temp, new Point(tileEngine.TileSizeInPixels.X, tileEngine.TileSizeInPixels.Y)));

            InitializeInput();
        }

        #region Layer metohds
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
        #endregion

        #region Tileset methods
        public void SelectTileset(string name)
        {
            tilesets.SelectTileset(name);
        }
        public void AddTileset(string name, string texturePath, Point sourceSize, Point offset)
        {
            // Get the texture resource.
            int id = texturePath.GetHashCode();

            Texture2D texture = null;

            // Either load it from a file or just get a reference to it.
            if (textures.ContainsResource(id))
            {
                // Get reference.
                texture = textures.GetReference(id);
            }
            else
            {
                // Load from file and give it to resource manager.
                texture = LoadTextureFromFile(texturePath);

                textures.AddResource(texturePath, texture);
            }

            // TODO: only adds textured tile sets.
            tilesets.AddTileset(new TexturedTileset(name, texture, sourceSize, offset));

            // TODO: test shiet.
            SelectLayer(layers.Layers.First().Name);
            SelectTileset(tilesets.Tilesets.First().Name);
            // TODO: test shiet.
        }
        public void RemoveTileset(string name)
        {
            Tileset tileset = tilesets.Tilesets.First(s => s.Name == name);

            tilesets.RemoveTileset(tileset);

            // Dereference the texture.
            textures.Dereference(tileset.Texture);
        }
        #endregion

        #region Editor methods
        // TODO: add animation methods.

        public override Rectangle GetMapBounds()
        {
            return tileEngine.PixelBounds;
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update(gameTime);

            layers.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            layers.Draw(spriteBatch, view.Bounds);

            components.Draw(spriteBatch, view.Bounds);

            spriteBatch.End();
        }
        #endregion
    }
}

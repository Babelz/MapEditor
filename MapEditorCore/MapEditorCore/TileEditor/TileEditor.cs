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
        private readonly LayerManager<TileLayer> layers;
        private readonly TilesetManager tilesets;

        private readonly TileEngine tileEngine;
        private readonly BasicView view;

        private readonly KeyboardInputListener keyboardInputListener;
        private readonly MouseInputListener mouseInputListener;
        private readonly InputManager inputManager;

        private Color backgroundColor;
        #endregion

        #region Properties
        public override IView View
        {
            get
            {
                return view;
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

        public LayerManager<TileLayer> LayerManager
        {
            get
            {
                return layers;
            }
        }
        public TilesetManager TilesetManager
        {
            get
            {
                return tilesets;
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

            Components.AddComponent(new BorderRenderer(this, temp));
            Components.AddComponent(new Grid(this, temp, new Point(tileEngine.TileSizeInPixels.X, tileEngine.TileSizeInPixels.Y)));

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
            if (TextureManager.ContainsResource(id))
            {
                // Get reference.
                texture = TextureManager.GetReference(id);
            }
            else
            {
                // Load from file and give it to resource manager.
                texture = LoadTextureFromFile(texturePath);

                TextureManager.AddResource(texturePath, texture);
            }

            // TODO: only adds textured tile sets.
            tilesets.AddTileset(new TexturedTileset(name, texture, sourceSize, offset));
        }
        public void RemoveTileset(string name)
        {
            Tileset tileset = tilesets.Tilesets.First(s => s.Name == name);

            tilesets.RemoveTileset(tileset);

            // Dereference the texture.
            TextureManager.Dereference(tileset.Texture);
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

            layers.Update(gameTime, view.Bounds);

            Components.Update(gameTime, view.Bounds);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            layers.Draw(spriteBatch, view.Bounds);

            Components.Draw(spriteBatch, view.Bounds);

            spriteBatch.End();
        }
        #endregion
    }
}

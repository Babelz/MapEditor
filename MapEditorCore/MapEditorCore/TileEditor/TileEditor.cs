using MapEditorCore.Abstractions;
using MapEditorCore.Components;
using MapEditorCore.Components.EditorComponents;
using MapEditorCore.TileEditor.Painting;
using MapEditorInput;
using MapEditorInput.Listener;
using MapEditorInput.Trigger;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private readonly Dictionary<Tileset, BrushBucket> brushBuckets;

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
        public override IEnumerable<Layer> Layers
        {
            get
            {
                return layers.Layers;
            }
        }

        public IEnumerable<Tileset> Tilesets
        {
            get
            {
                return tilesets.Tilesets;
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

        #region Events
        /*
         * Delegates add and remove calls to layer manager.
         */

        public override event LayerManagerEventHandler LayerAdded
        {
            add
            {
                layers.LayerAdded += value;
            }
            remove
            {
                layers.LayerAdded -= value;
            }
        }
        public override event LayerManagerEventHandler LayerRemoved
        {
            add
            {
                layers.LayerRemoved += value;
            }
            remove
            {
                layers.LayerRemoved -= value;
            }
        }
        
        /*
         * Delegates add and remove calls to tileset manager.
         */
        public event TilesetEventHandler TilesetAdded
        {
            add
            {
                tilesets.TilesetAdded += value;
            }
            remove
            {
                tilesets.TilesetAdded -= value;
            }
        }
        public event TilesetEventHandler TilesetRemoved
        {
            add
            {
                tilesets.TilesetRemoved += value;
            }
            remove
            {
                tilesets.TilesetRemoved -= value;
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

            brushBuckets = new Dictionary<Tileset, BrushBucket>();

            backgroundColor = Color.CornflowerBlue;
        }

        private void InitializeInput()
        {
            mouseInputListener.Map("paint", (args) => 
            {
                if (!layers.HasLayerSelected) return;
                if (!tilesets.HasTilesetSelected) return;

                // Get brush bucket.
                BrushBucket brushBucket = brushBuckets[tilesets.SelectedTileset];

                // Return if no brush is selected.
                if (!brushBucket.HasBrushSelected) return;

                int mouseX = Mouse.GetState().X;
                int mouseY = Mouse.GetState().Y;
                
                // Check that mouse is inside editors view port.
                if (!SpriteBatch.GraphicsDevice.Viewport.Bounds.Contains(mouseX, mouseY)) return;

                // Calculate index.
                mouseX = mouseX / tileEngine.TileSizeInPixels.X;
                mouseY = mouseY / tileEngine.TileSizeInPixels.Y;
                
                // Check that the index is in bounds.
                if (mouseX < 0 || mouseX >= layers.SelectedLayer.Width) return;
                if (mouseY < 0 || mouseY >= layers.SelectedLayer.Height) return;

                // Get brush and paint with it.
                TileBrush brush = brushBucket.SelectedBrush;

                while (brush.CanPaint())
                {
                    PaintArgs paintArgs = brush.Paint();

                    layers.SelectedLayer.TileAtIndex(mouseX, mouseY).Paint(paintArgs);    
                }

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
            TexturedTileset tileset = new TexturedTileset(name, texture, sourceSize, offset);
            tilesets.AddTileset(tileset);

            // Add new brush bucket for this tileset.
            brushBuckets.Add(tileset, new BrushBucket(tileset));
        }
        public void RemoveTileset(string name)
        {
            Tileset tileset = tilesets.Tilesets.First(s => s.Name == name);

            tilesets.RemoveTileset(tileset);

            // Remove brush bucket.
            brushBuckets.Remove(tileset);

            // Dereference the texture.
            TextureManager.Dereference(tileset.Texture);
        }

        public string GetTexturePath(Texture2D texture)
        {
            return TextureManager.PathToResource(texture);
        }
        #endregion

        #region Brush methods
        public void SelectBrush(string name)
        {
            if (!tilesets.HasTilesetSelected) return;

            brushBuckets[tilesets.SelectedTileset].SelectBrush(name);
        }
        public BrushBucket GetBrushBucketForSelectedTileset()
        {
            if (!tilesets.HasTilesetSelected) return null;

            return brushBuckets[tilesets.SelectedTileset];
        }
        #endregion

        #region Editor methods
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

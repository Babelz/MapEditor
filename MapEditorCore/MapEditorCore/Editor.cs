using MapEditorCore.Abstractions;
using MapEditorCore.Components;
using MapEditorCore.Components.EditorComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MapEditorCore
{
    /// <summary>
    /// Common interface for every editor.
    /// </summary>
    public abstract class Editor : IDisposable
    {
        #region Fields
        private ContentManager content;
        private SpriteBatch spriteBatch;
        
        private bool disposed;
        #endregion

        #region Properties
        public abstract IEnumerable<Layer> Layers
        {
            get;
        }
        public abstract IEnumerable<EditorComponent> Components
        {
            get;
        }
        public abstract Color BackgroundColor
        {
            get;
            set;
        }

        protected ContentManager Content
        {
            get
            {
                return content;
            }
        }
        protected SpriteBatch SpriteBatch
        {
            get
            {
                return spriteBatch;
            }
        }
        #endregion

        public Editor()
        {
        }

        /// <summary>
        /// Load all content and do initialization before editor "starts".
        /// Its safe to use editors sprite batch and content at this point.
        /// </summary>
        protected abstract void OnInitialize();

        /// <summary>
        /// Initialize the editor, pass content and spritebatch for it.
        /// </summary>
        /// <param name="content">xna content manager</param>
        /// <param name="spriteBatch">xna sprite batch</param>
        public void Initialize(ContentManager content, SpriteBatch spriteBatch)
        {
            this.content = content;
            this.spriteBatch = spriteBatch;

            OnInitialize();
        }

        protected Texture2D LoadFromFile(string path)
        {
            Texture2D texture = null;

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                texture = Texture2D.FromStream(spriteBatch.GraphicsDevice, stream);    
            }

            return texture;
        }

        protected virtual void OnDispose()
        {
        }

        public abstract void SelectLayer(string name);
        public abstract void AddLayer(string name, Point size);
        public abstract void RemoveLayer(string name);

        public abstract Rectangle GetMapBounds();

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);

        public void Dispose()
        {
            if (disposed) return;

            OnDispose();

            // No need to finalize.
            GC.SuppressFinalize(this);

            disposed = true;
        }

        ~Editor()
        {
            Dispose();
        }
    }
}

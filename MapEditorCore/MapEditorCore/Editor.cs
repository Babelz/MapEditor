using MapEditorCore.Abstractions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore
{
    /// <summary>
    /// Common interface for every editor.
    /// </summary>
    public abstract class Editor
    {
        #region Fields
        private ContentManager content;
        private SpriteBatch spriteBatch;
        #endregion

        #region Properties
        public abstract IEnumerable<Layer> Layers
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

        public abstract void MakeLayerActive(string name);
        public abstract void AddLayer(string name, Point size);
        public abstract void RemoveLayer(string name);

        public abstract Rectangle GetMapBounds();

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}

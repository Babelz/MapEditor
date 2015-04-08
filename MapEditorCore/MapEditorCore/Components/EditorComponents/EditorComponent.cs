using MapEditorCore.Abstractions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.Components.EditorComponents
{
    /// <summary>
    /// Base class for editor related components.
    /// </summary>
    public abstract class EditorComponent
    {
        #region Fields
        private readonly Editor editor;

        private DrawOrder drawOrder;

        private bool enabled;
        private bool visible;
        #endregion

        #region Properties
        public DrawOrder DrawOrder
        {
            get
            {
                return drawOrder;
            }
            set
            {
                drawOrder = value;
            }
        }
        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;
            }
        }
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
            }
        }

        protected Editor Editor
        {
            get
            {
                return editor;
            }
        }
        #endregion


        public EditorComponent(Editor editor)
        {
            this.editor = editor;

            enabled = true;
            visible = true;

            drawOrder = new DrawOrder();
        }

        /// <summary>
        /// Called once every update if component is enabled.
        /// </summary>
        protected virtual void OnUpdate(GameTime gameTime)
        {
        }
        /// <summary>
        /// Called once every draw if component is visible.
        /// </summary>
        protected virtual void OnDraw(SpriteBatch spriteBatch, Rectangle viewBounds)
        {
        }

        public void Update(GameTime gameTime)
        {
            if (!enabled) return;

            OnUpdate(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch, Rectangle viewBounds)
        {
            if (!visible) return;

            OnDraw(spriteBatch, viewBounds);
        }
    }
}

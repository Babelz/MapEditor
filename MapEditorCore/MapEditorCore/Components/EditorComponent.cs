using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.Components
{
    public abstract class EditorComponent
    {
        #region Fields
        private readonly string displayName;
        private readonly Editor editor;

        private int drawOrder;

        private bool enabled;
        private bool visible;
        #endregion

        #region Properties
        public string DisplayName
        {
            get
            {
                return displayName;
            }
        }
        public int DrawOrder
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


        public EditorComponent(string displayName, Editor editor)
        {
            this.displayName = displayName;
            this.editor = editor;

            enabled = true;
            visible = true;
        }

        protected virtual void OnUpdate(GameTime gameTime)
        {
        }
        protected virtual void OnDraw(SpriteBatch spriteBatch)
        {
        }

        public void Update(GameTime gameTime)
        {
            if (!enabled) return;

            OnUpdate(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!visible) return;

            OnDraw(spriteBatch);
        }
    }
}

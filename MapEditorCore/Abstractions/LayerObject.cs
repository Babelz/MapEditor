using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorCore.Abstractions
{
    public abstract class LayerObject
    {
        #region Fields
        private Point position;

        private bool visible;
        private bool enalbed;
        #endregion

        #region Properties
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = true;
            }
        }
        public bool Enabled
        {
            get
            {
                return enalbed;
            }
            set
            {
                enalbed = true;
            }
        }
        public int X
        {
            get
            {
                return position.X;
            }
            set
            {
                Point oldPosition = position;
                position.X = value;

                if (position != oldPosition)
                    OnMoved(oldPosition, position);
            }
        }
        public int Y
        {
            get
            {
                return position.Y;
            }
            set
            {
                Point oldPosition = position;
                position.Y = value;

                if (position != oldPosition)
                    OnMoved(oldPosition, position);
            }
        }
        #endregion

        public LayerObject()
        {
            visible = true;
            enalbed = true;
        }

        /// <summary>
        /// Called when this object gets moved.
        /// </summary>
        /// <param name="oldPosition">old position of this object</param>
        /// <param name="newPosition">new position of this object</param>
        protected virtual void OnMoved(Point oldPosition, Point newPosition)
        {
        }

        protected virtual void OnUpdate(GameTime gameTime)
        {
        }
        protected virtual void OnDraw(SpriteBatch spriteBatch)
        {
        }

        public void Update(GameTime gameTime)
        {
            if (!enalbed) return;

            OnUpdate(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!visible) return;

            OnDraw(spriteBatch);
        }
    }
}

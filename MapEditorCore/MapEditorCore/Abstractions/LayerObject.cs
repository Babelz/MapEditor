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
        #endregion

        #region Properties
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
                    if (Moved != null) Moved(oldPosition, position);
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
                    if (Moved != null) Moved(oldPosition, position);
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Called when this object is moved.
        /// </summary>
        public event LayerObjectMovedEventHandler Moved;
        #endregion

        public LayerObject()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public delegate void LayerObjectMovedEventHandler(Point oldPosition, Point newPosition);
    }
}

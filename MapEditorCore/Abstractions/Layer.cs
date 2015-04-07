using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorCore.Abstractions
{
    public enum LayerType
    {
        /// <summary>
        /// Layer cant be modified.
        /// </summary>
        Static,

        /// <summary>
        /// Layer can be modified.
        /// </summary>
        Dynamic
    }

    /// <summary>
    /// Bases class for all layers.
    /// </summary>
    public abstract class Layer
    {
        #region Static fields
        private static int idCounter;
        #endregion

        #region Fields
        private readonly int id;

        private bool enabled;
        private bool visible;
        
        private string name;

        private LayerType type;

        private Point position;
        private Point size;

        private int drawOrder;
        #endregion

        #region Properties
        /// <summary>
        /// Name of the layer.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        /// <summary>
        /// Unique ID of the layer.
        /// </summary>
        public int ID
        {
            get
            {
                return id;
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
                position.X = value;
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
                position.Y = value;
            }
        }
        public int Width 
        {
            get
            {
                return size.X;
            }
        }
        public int Height
        {
            get
            {
                return size.Y;
            }
        }
        /// <summary>
        /// Gets or sets layers enable value.
        /// </summary>
        public bool Enalbed
        {
            get
            {
                return enabled;
            }
            set
            {
                bool oldEnabled = enabled;

                enabled = value;

                if (oldEnabled == enabled) return;

                if (!enabled)
                    OnDisable();

                if (enabled)
                    OnEnable();
            }
        }
        /// <summary>
        /// Gets or sets layers visible value.
        /// </summary>
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                bool oldVisible = visible;
                
                visible = value;

                if (oldVisible == visible) return;

                if (!visible)
                    OnHide();

                if (visible)
                    OnShow();
            }
        }
        /// <summary>
        /// Gets or sets layers type.
        /// </summary>
        public LayerType Type
        {
            get
            {
                return type;
            }
            set
            {
                LayerType oldType = type;

                if (oldType == type) return;

                switch (type)
                {
                    case LayerType.Static:
                        MakeStatic();
                        return;
                    case LayerType.Dynamic:
                        MakeDynamic();
                        return;
                    default:
                        return;
                }
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
        #endregion

        static Layer()
        {
            idCounter = 0;
        }

        public Layer(string name, Point size)
        {
            this.name = name;
            this.size = size;

            id = idCounter;
            idCounter++;

            enabled = true;
            visible = true;
        }
        
        /// <summary>
        /// Called when layer gets disabled.
        /// </summary>
        protected virtual void OnDisable()
        {
        }
        /// <summary>
        /// Called when layer gets enabled.
        /// </summary>
        protected virtual void OnEnable()
        {
        }

        /// <summary>
        /// Called when layer gets hidden.
        /// </summary>
        protected virtual void OnHide()
        {
        }
        /// <summary>
        /// Called when layer is shown.
        /// </summary>
        protected virtual void OnShow()
        {
        }

        /// <summary>
        /// Called when layer type changes to static.
        /// </summary>
        protected abstract void MakeStatic();
        /// <summary>
        /// Called when layer type changes to dynamic.
        /// </summary>
        protected abstract void MakeDynamic();

        /// <summary>
        /// Called once every update.
        /// </summary>
        /// <param name="gameTime">current game time</param>
        protected abstract void OnUpdate(GameTime gameTime);
        /// <summary>
        /// Called once every draw.
        /// </summary>
        /// <param name="spriteBatch">sprite batch</param>
        protected abstract void OnDraw(SpriteBatch spriteBatch);

        /// <summary>
        /// Resize this layer.
        /// </summary>
        /// <param name="newSize">wanted size</param>
        protected virtual void Resize(Point newSize)
        {
            size = newSize;
        }
        /// <summary>
        /// Move the layer to wanted position.
        /// </summary>
        /// <param name="newPosition">new position of the layer</param>
        protected virtual void MoveTo(Point newPosition)
        {
            position = newPosition;
        }
        /// <summary>
        /// Move the layer by given amount.
        /// </summary>
        /// <param name="amount">amount to move this layer</param>
        protected virtual void MoveBy(Point amount)
        {
            position += amount;
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

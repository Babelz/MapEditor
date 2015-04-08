using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.Abstractions
{
    /// <summary>
    /// Class that is used to keep rendering sequence of specific group of 
    /// objects in wanted order at all times.
    /// </summary>
    public sealed class DrawOrder
    {
        #region Fields
        private int value;
        #endregion

        #region Properites
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                if (this.value != value)
                {
                    this.value = value;

                    if (Changed != null) Changed();
                }
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Called when order gets changed.
        /// </summary>
        public event DrawOrderEventHandler Changed;
        #endregion

        public DrawOrder()
        {
        }

        public static bool operator >(DrawOrder a, DrawOrder b) 
        {
            return a.value > b.value;
        }
        public static bool operator <(DrawOrder a, DrawOrder b)
        {
            return a.value < b.value;
        }
        public static bool operator ==(DrawOrder a, DrawOrder b)
        {
            return a.value == b.value;
        }
        public static bool operator !=(DrawOrder a, DrawOrder b)
        {
            return !(a.value == b.value);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(DrawOrder)) return false;

            DrawOrder other = obj as DrawOrder;

            return this == other;
        }
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public delegate void DrawOrderEventHandler();
    }
}

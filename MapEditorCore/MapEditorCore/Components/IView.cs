﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorCore.Components
{
    /// <summary>
    /// Interface for cameras.
    /// </summary>
    public interface IView
    {
        #region Properties
        /// <summary>
        /// Bounds of this view.
        /// </summary>
        Rectangle Bounds
        {
            get;
        }
        #endregion

        void SetArea(int width, int height);
    }
}

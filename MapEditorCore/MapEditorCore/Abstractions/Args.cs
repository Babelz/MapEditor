using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.Abstractions
{
    public sealed class LayerManagerEventArgs : EventArgs
    {
        #region Fields
        private readonly Layer layer;
        #endregion

        #region Properties
        public Layer Layer
        {
            get
            {
                return layer;
            }
        }
        #endregion

        public LayerManagerEventArgs(Layer layer)
        {
            this.layer = layer;
        }
    }
}

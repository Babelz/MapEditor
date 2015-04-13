using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.Abstractions
{
    /// <summary>
    /// Base class for all layer managers.
    /// </summary>
    public interface ILayerManager
    {
        #region Properties
        /// <summary>
        /// Get all layers. Exposed for view models.
        /// </summary>
        IEnumerable<Layer> Layers
        {
            get;
        }
        #endregion

        #region Events
        /// <summary>
        /// Called when layer is added to the manager.
        /// </summary>
        event LayerEventHandler LayerAdded;

        /// <summary>
        /// Called when layer is removed from the manager.
        /// </summary>
        event LayerEventHandler LayerRemoved;
        #endregion
    }
}

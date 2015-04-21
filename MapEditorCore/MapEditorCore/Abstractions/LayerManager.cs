using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorCore.Abstractions
{
    public sealed class LayerManager<T> where T : Layer
    {
        #region Fields
        private readonly List<T> layers;

        private T selectedLayer;
        #endregion

        #region Properties
        /// <summary>
        /// Get all layers. Exposed for view models.  
        /// </summary>
        public IEnumerable<T> Layers
        {
            get
            {
                return layers;
            }
        }
        public bool HasLayerSelected
        {
            get
            {
                return selectedLayer != null;
            }
        }
        /// <summary>
        /// Gets selected layer. Exposed for view models.
        /// </summary>
        public T SelectedLayer
        {
            get
            {
                return selectedLayer;
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Called when layer is added to the manager.
        /// </summary>
        public event LayerManagerEventHandler LayerAdded;

        /// <summary>
        /// Called when layer is removed from the manager.
        /// </summary>
        public event LayerManagerEventHandler LayerRemoved;
        #endregion

        public LayerManager()
        {
            layers = new List<T>();
        }

        #region Event handlers
        private void DrawOrder_Changed()
        {
            SortLayers();
        }
        #endregion

        private void SortLayers()
        {
            layers.Sort((a, b) =>
            {
                if (a.DrawOrder == b.DrawOrder) return 0;
                if (a.DrawOrder > b.DrawOrder) return -1;

                return 1;
            });
        }

        /// <summary>
        /// Adds the given layer.
        /// </summary>
        /// <param name="layer">layer to add</param>
        public void AddLayer(T layer)
        {
            layers.Add(layer);

            SortLayers();

            layer.DrawOrder.Changed += DrawOrder_Changed;

            if (LayerAdded != null) LayerAdded(this, new LayerManagerEventArgs(layer));
        }
        /// <summary>
        /// Removes the given layer.
        /// </summary>
        /// <param name="layer">layer to remove</param>
        public void RemoveLayer(T layer)
        {
            layers.Remove(layer);

            layer.DrawOrder.Changed -= DrawOrder_Changed;

            if (ReferenceEquals(layer, selectedLayer)) selectedLayer = null;

            if (LayerRemoved != null) LayerRemoved(this, new LayerManagerEventArgs(layer));
        }
        /// <summary>
        /// Removes layer with given name.
        /// </summary>
        /// <param name="name">name of the layer to remove</param>
        public void RemoveLayer(string name)
        {
            T layer = layers.FirstOrDefault(l => l.Name == name);

            RemoveLayer(layer);
        }

        /// <summary>
        /// Selects given layer.
        /// </summary>
        /// <param name="name">name of the layer to activate, if name is empty or null, clears selection</param>
        public void SelectLayer(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                selectedLayer = null;
                
                return;
            }

            selectedLayer = layers.FirstOrDefault(l => l.Name == name);
        }

        /// <summary>
        /// Update all layers.
        /// </summary>
        public void Update(GameTime gameTime, Rectangle viewBounds)
        {
            for (int i = 0; i < layers.Count; i++) layers[i].Update(gameTime, viewBounds);
        }
        /// <summary>
        /// Draw all layers.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch, Rectangle viewBounds)
        {
            for (int i = 0; i < layers.Count; i++) layers[i].Draw(spriteBatch, viewBounds);
        }
    }
}

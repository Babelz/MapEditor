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

        private T activeLayer;
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
        /// <summary>
        /// Gets active layer. Exposed for view models.
        /// </summary>
        public T ActiveLayer
        {
            get
            {
                return activeLayer;
            }
        }
        #endregion

        public LayerManager()
        {
            layers = new List<T>();
        }

        /// <summary>
        /// Adds the given layer.
        /// </summary>
        /// <param name="layer">layer to add</param>
        public void AddLayer(T layer)
        {
            layers.Add(layer);
        }
        /// <summary>
        /// Removes the given layer.
        /// </summary>
        /// <param name="layer">layer to remove</param>
        public void RemoveLayer(T layer)
        {
            layers.Remove(layer);

            if (ReferenceEquals(layer, activeLayer)) activeLayer = null;
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
        /// Activates given layer.
        /// </summary>
        /// <param name="name">name of the layer to activate</param>
        public void MakeActive(string name)
        {
            activeLayer = layers.FirstOrDefault(l => l.Name == name);
        }

        /// <summary>
        /// Update all layers.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < layers.Count; i++) layers[i].Update(gameTime);
        }
        /// <summary>
        /// Draw all layers.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < layers.Count; i++) layers[i].Draw(spriteBatch);
        }
    }
}

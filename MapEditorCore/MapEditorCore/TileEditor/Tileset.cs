using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.TileEditor
{
    /// <summary>
    /// Base class for all tile sets. Can be used for texture or 
    /// animation mapping.
    /// </summary>
    public abstract class Tileset : IDisposable
    {
        #region Fields
        private readonly Texture2D texture;
        private readonly Point sourceSize;

        private bool disposed;
        #endregion

        #region Properties
        public bool Disposed
        {
            get
            {
                return disposed;
            }
        }
        /// <summary>
        /// Texture this set is using.
        /// </summary>
        public Texture2D Texture
        {
            get
            {
                return texture;
            }
        }
        protected Point SourceSize
        {
            get
            {
                return sourceSize;
            }
        }
        #endregion

        #region Events
        public event EventHandler Disposing;
        #endregion

        public Tileset(Texture2D texture, Point sourceSize)
        {
            this.texture = texture;
            this.sourceSize = sourceSize;

            GenerateSources();
        }

        /// <summary>
        /// Called when set gets disposed.
        /// </summary>
        protected virtual void OnDispose()
        {
        }

        protected abstract void GenerateSources();

        public abstract Rectangle GetSource(Point sourceIndex);

        /// <summary>
        /// Dispose the set.
        /// </summary>
        public void Dispose()
        {
            if (disposed) return;

            if (Disposing != null) Disposing(this, new EventArgs());

            texture.Dispose();

            OnDispose();

            GC.SuppressFinalize(this);

            disposed = true;
        }

        ~Tileset()
        {
            Dispose();
        }
    }
}

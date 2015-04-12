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
    public abstract class Tileset
    {
        #region Fields
        private readonly Texture2D texture;

        private readonly Point sourceSize;
        private readonly Point offset;

        private string name;

        private bool disposed;
        #endregion

        #region Properties
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

        /// <summary>
        /// Size of one source on the set.
        /// </summary>
        protected Point SourceSize
        {
            get
            {
                return sourceSize;
            }
        }
        /// <summary>
        /// Offset of indices.
        /// </summary>
        protected Point Offset
        {
            get
            {
                return offset;
            }
        }
        #endregion

        #region Events
        public event EventHandler Disposing;
        #endregion

        public Tileset(string name, Texture2D texture, Point sourceSize, Point offset)
        {
            this.name = name;
            this.texture = texture;
            this.sourceSize = sourceSize;
            this.offset = offset;

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
    }
}

﻿using Microsoft.Xna.Framework;
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
        private readonly Point indicesCount;

        private string name;

        private bool deleted;
        #endregion

        #region Properties
        /// <summary>
        /// Offset of indices.
        /// </summary>
        public Point Offset
        {
            get
            {
                return offset;
            }
        }
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
        public Point SourceSize
        {
            get
            {
                return sourceSize;
            }
        }
        /// <summary>
        /// Count of rows and columns.
        /// </summary>
        public Point IndicesCount
        {
            get
            {
                return indicesCount;
            }
        }
        public bool Deleted
        {
            get
            {
                return deleted;
            }
        }
        #endregion

        #region Events
        public event TilesetEventHandler Deleting;
        #endregion

        public Tileset(string name, Texture2D texture, Point sourceSize, Point offset)
        {
            this.name = name;
            this.texture = texture;
            this.sourceSize = sourceSize;
            this.offset = offset;

            // Calculate offset values.
            int sourceOffsetX = (texture.Width - offset.X) % sourceSize.X;
            int sourceOffsetY = (texture.Height - offset.Y) % sourceSize.Y;

            sourceOffsetX = sourceOffsetX > 0 ? sourceSize.X : 0;
            sourceOffsetY = sourceOffsetY > 0 ? sourceSize.Y : 0;

            sourceOffsetX += 1;
            sourceOffsetY += 1;

            // Calculate indices count (rows & columns count).
            indicesCount = new Point((texture.Width - offset.X + sourceOffsetX) / sourceSize.X,
                                     (texture.Height - offset.Y + sourceOffsetY) / sourceSize.Y);

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
        /// Notifies all set users that this set is being deleted.
        /// </summary>
        public void Delete()
        {
            if (deleted) return;

            if (Deleting != null) Deleting(this, new TilesetEventArgs(this));

            deleted = true;
        }
    }
}

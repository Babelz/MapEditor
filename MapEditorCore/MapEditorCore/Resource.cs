using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore
{
    public sealed class Resource<T> : IReferenceWrapper<T> where T : IDisposable
    {
        #region Fields
        private readonly T value;

        private readonly string path;
        private readonly int id;

        private int references;

        private bool disposed;
        #endregion

        #region Properties
        public T Value
        {
            get
            {
                return value;
            }
        }
        IDisposable IReferenceWrapper.Value
        {
            get
            {
                return value as IDisposable;
            }
        }
        /// <summary>
        /// Unique resource identifier.
        /// </summary>
        public int ID
        {
            get
            {
                return id;
            }
        }
        /// <summary>
        /// Path to this resource.
        /// </summary>
        public string Path
        {
            get
            {
                return path;
            }
        }
        #endregion

        #region Events
        public event ReferenceDisposingEventHander Disposing;
        #endregion

        public Resource(string path, T value)
        {
            this.path = path;
            this.value = value;

            id = path.GetHashCode();
        }

        public void Reference()
        {
            references++;
        }

        public void DeReference()
        {
            references--;

            if (references <= 0) Dispose();
        }

        public void Dispose()
        {
            if (disposed) return;

            disposed = true;

            if (Disposing != null) Disposing(this);

            value.Dispose();

            GC.SuppressFinalize(this);
        }

        ~Resource()
        {
            Dispose();
        }
    }
}

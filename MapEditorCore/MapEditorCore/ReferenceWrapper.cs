using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore
{
    public interface IReferenceCountable
    {
        void Reference();
        void DeReference();
    }

    /// <summary>
    /// Interface for non-generic reference counting.
    /// </summary>
    public interface IReferenceWrapper : IDisposable, IReferenceCountable
    {
        #region Properties
        /// <summary>
        /// Returns wrapped reference. Does not increase
        /// the reference counter.
        /// </summary>
        IDisposable Value
        {
            get;
        }
        #endregion

        #region Events
        /// <summary>
        /// Called when reference is getting disposed.
        /// </summary>
        event ReferenceDisposingEventHander Disposing;
        #endregion
    }

    /// <summary>
    /// Interface for generic reference counting.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReferenceWrapper<T> : IReferenceWrapper where T : IDisposable
    {
        #region Properties
        /// <summary>
        /// Returns the wrapped reference. Does not increase 
        /// the reference counter.
        /// </summary>
        new T Value
        {
            get;
        }
        #endregion
    }

    /// <summary>
    /// Non-generic reference wrapper.
    /// </summary>
    public class ReferenceWrapper : IReferenceWrapper
    {
        #region Fields
        private readonly IDisposable value;
        
        private int references;

        private bool disposed;
        #endregion

        #region Properties
        public IDisposable Value
        {
            get
            {
                return value;
            }
        }
        #endregion

        #region Events
        public event ReferenceDisposingEventHander Disposing;
        #endregion

        public ReferenceWrapper(IDisposable value)
        {
            this.value = value;
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
    }

    /// <summary>
    /// Simple generic reference wrapper.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReferenceWrapper<T> : IReferenceWrapper<T> where T : IDisposable
    {
        #region Fields
        private readonly T value;

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
        #endregion

        #region Events
        public event ReferenceDisposingEventHander Disposing;
        #endregion 

        public ReferenceWrapper(T value)
        {
            this.value = value;
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

        ~ReferenceWrapper()
        {
            Dispose();
        }
    }

    public delegate void ReferenceDisposingEventHander(IReferenceWrapper reference);
}

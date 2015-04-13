using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore
{
    /// <summary>
    /// Class for storing and managing
    /// shared disposable resources.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ResourceManager<T> where T : IDisposable
    {
        #region Fields
        private readonly List<Resource<T>> resources;
        #endregion

        public ResourceManager()
        {
            resources = new List<Resource<T>>();
        }

        #region Event handlers
        private void resource_Disposing(IReferenceWrapper reference)
        {
            reference.Disposing -= resource_Disposing;

            Resource<T> resource = reference as Resource<T>;

            resources.Remove(resource);
        }
        #endregion

        /// <summary>
        /// Adds resource to this manager.
        /// </summary>
        /// <param name="path">path to the resource, resources id will be this string values hash code</param>
        /// <param name="value">resource value</param>
        public void AddResource(string path, T value)
        {
            int hash = path.GetHashCode();

            if (resources.Find(r => r.ID == hash) != null) throw new InvalidOperationException("Duplicated resource ID.");

            Resource<T> resource = new Resource<T>(path, value);
            resource.Disposing += resource_Disposing;

            resources.Add(resource);
        }
        /// <summary>
        /// Returns a reference to wanted resource.
        /// </summary>
        /// <param name="id">id of the resource</param>
        /// <returns>resource reference</returns>
        public T GetReference(int id)
        {
            Resource<T> resource = resources.FirstOrDefault(r => r.ID == id);
            resource.Reference();

            return resource.Value;
        }

        /// <summary>
        /// Dereferences a resource.
        /// </summary>
        /// <param name="id">id of the resource</param>
        public void Dereference(int id)
        {
            Resource<T> resource = resources.FirstOrDefault(r => r.ID == id);
            resource.DeReference();
        }
        /// <summary>
        /// Dereferences a resource.
        /// </summary>
        /// <param name="value">resource to dereference</param>
        public void Dereference(T value)
        {
            Resource<T> resource = resources.FirstOrDefault(v => ReferenceEquals(value, v.Value));
            resource.DeReference();
        }

        /// <summary>
        /// Returns true if container contains given resource.
        /// </summary>
        /// <param name="id">id of the resource</param>
        public bool ContainsResource(int id)
        {
            return resources.Find(r => r.ID == id) != null;
        }
        /// <summary>
        /// Returns true if container contains given resource.
        /// </summary>
        /// <param name="resource">resource to validate</param>
        public bool ContainsResource(T resource)
        {
            return resources.Find(r => ReferenceEquals(r, resource)) != null;
        }
    }
}

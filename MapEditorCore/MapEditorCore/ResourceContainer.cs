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

        public void AddResource(string path, T value)
        {
            int hash = path.GetHashCode();

            if (resources.Find(r => r.ID == hash) != null) throw new InvalidOperationException("Duplicated resource ID.");

            Resource<T> resource = new Resource<T>(path, value);
            resource.Disposing += resource_Disposing;

            resources.Add(resource);
        }
        
        public T GetReference(int id)
        {
            Resource<T> resource = resources.FirstOrDefault(r => r.ID == id);
            resource.Reference();

            return resource.Value;
        }

        public void Dereference(int id)
        {
            Resource<T> resource = resources.FirstOrDefault(r => r.ID == id);
            resource.DeReference();
        }
        public void Dereference(T value)
        {
            Resource<T> resource = resources.FirstOrDefault(v => ReferenceEquals(value, v.Value));
            resource.DeReference();
        }

        public bool ContainsResource(int id)
        {
            return resources.Find(r => r.ID == id) != null;
        }
    }
}

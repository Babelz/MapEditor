using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Metadata
{
    /// <summary>
    /// Single field of metadata object. 
    /// </summary>
    public sealed class MetadataField
    {
        #region Fields
        // Values runtime type.
        private readonly Type typeOfValue;

        // Name of the field. Can be translated to index.
        private readonly string name;

        // Value contained in this field.
        private object value;
        #endregion

        #region Properties
        public string Name
        {
            get
            {
                return name;
            }
        }
        /// <summary>
        /// Type of the value.
        /// </summary>
        public Type TypeOfValue
        {
            get
            {
                return typeOfValue;
            }
        }
        #endregion

        /// <param name="name">name of the field</param>
        /// <param name="value">value of the field</param>
        public MetadataField(string name, object value)
        {
            this.name = name;
            this.value = value;
            
            typeOfValue = value.GetType();
        }

        /// <summary>
        /// Attempts to read the value of this field.
        /// </summary>
        /// <typeparam name="T">type of of the value to be returned</typeparam>
        /// <returns>value</returns>
        /// <exception cref="InvalidCastException">Type mismatch.</exception>
        public T GetValue<T>() 
        {
            return (T)value;
        }

        /// <summary>
        /// Sets the fields value.
        /// </summary>
        /// <param name="value">new value</param>
        public void SetValue(object value)
        {
            this.value = value;
        }
    }
}

/*
 *          MetadataObject
 * Wraps fields to represent abstract data structures.
 * Structures are type "safe". Type checking is performed 
 * at this level. Fields are not type safe by default.
 * 
 * Will throw an exception if there is a type mismatch with a field.
 * Will throw an exception if a field with given name is not found.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Metadata
{
    public sealed class MetadataObject
    {
        #region Fields
        private readonly MetadataField[] fields;

        private readonly string[] indices;

        private readonly string name;
        #endregion

        #region Properties
        public string Name
        {
            get
            {
                return name;
            }
        }
        #endregion

        /// <param name="name">name of this object, needs to be unique</param>
        /// <param name="fields">fields of this object</param>
        public MetadataObject(string name, MetadataField[] fields)
        {
            this.name = name;
            this.fields = fields;

            // Create indices.
            indices = new string[fields.Length];

            for (int i = 0; i < indices.Length; i++) indices[i] = fields[i].Name;
        }

        /// <summary>
        /// Creates new instance of metadata object by copying the
        /// fields of another object.
        /// </summary>
        /// <param name="name">name of the new object, needs to be unique</param>
        /// <param name="other">object whose fields will be copied</param>
        /// <exception cref="MetadataSystemException">Name not unique.</exception>
        public MetadataObject(string name, MetadataObject other)
            : this(name, other.fields)
        {
            if (name == other.name)
            {
                throw MetadataSystemException.NameNotUnique(name);
            }
        }

        /// <summary>
        /// Attempts to read a field value from this object. 
        /// </summary>
        /// <typeparam name="T">type of the field</typeparam>
        /// <param name="fieldName">name of the field</param>
        /// <returns>fields value</returns>
        /// <exception cref="MetadataSystemException">Field type mismatch.</exception>
        /// <exception cref="MetadataSystemException">Field not found.</exception>
        public T GetFieldValue<T>(string fieldName)
        {
            Type type = typeof(T);

            int index = Array.IndexOf<string>(indices, fieldName);

            if (index != -1)
            {
                MetadataField field = fields[index];

                if (field.TypeOfValue == type)
                {
                    return field.GetValue<T>();
                }

                // Field type mismatch.
                throw MetadataSystemException.TypeMismatch(name, fieldName, type, field.TypeOfValue);
            }

            // Field not found.
            throw MetadataSystemException.FieldNotFound(name, fieldName);
        }

        /// <summary>
        /// Attempts to set the fields value.
        /// </summary>
        /// <param name="value">new value of the field</param>
        /// <exception cref="MetadataSystemException">Field type mismatch.</exception>
        /// <exception cref="MetadataSystemException">Field not found.</exception>
        public void SetFieldValue(string fieldName, object value)
        {
            Type type = value.GetType();

            int index = Array.IndexOf<string>(indices, fieldName);

            if (index != -1)
            {
                MetadataField field = fields[index];

                if (field.TypeOfValue == type)
                {
                    field.SetValue(value);
                }
                else
                {
                    // Field type mismatch.
                    throw MetadataSystemException.TypeMismatch(name, fieldName, type, field.TypeOfValue);
                }
            }
            else
            {
                // Field not found.
                throw MetadataSystemException.FieldNotFound(name, fieldName);
            }
        }
    }
}

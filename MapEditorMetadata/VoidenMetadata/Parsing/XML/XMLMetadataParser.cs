using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MapEditor.Metadata.Parsing.XML
{
    // TODO: document this class.
    public sealed class XMLMetadataParser : MetadataParser<XDocument>
    {
        #region Statics
        private static readonly string NAME_ATTRIB = "Name";
        private static readonly string FIELD_ATTRIB = "Field";
        private static readonly string TYPE_ATTRIB = "Type";
        private static readonly string VALUE_ATTRIB = "Value";
        private static readonly string OBJECT_DESC = "Object";

        private static readonly string TYPENAME_STRING = "string";
        private static readonly string TYPENAME_INT = "int";
        private static readonly string TYPENAME_BOOL = "bool";
        private static readonly string TYPENAME_BYTE = "byte";
        private static readonly string TYPENAME_FLOAT = "float";
        private static readonly string TYPENAME_OBJECT = "object";

        private static readonly string[] PRIMITIVE_TYPENAMES = new string[] { TYPENAME_STRING, TYPENAME_INT, TYPENAME_BOOL, TYPENAME_BYTE, TYPENAME_FLOAT };
        #endregion

        #region Properties
        public override string FileFormat
        {
            get
            {
                return XMLInformation.FILE_FORMAT;
            }
        }
        public override string Name
        {
            get
            {
                return XMLInformation.NAME;
            }
        }
        #endregion

        public XMLMetadataParser()
            : base()
        {
        }

        #region Parsing methods
        private bool IsPrimitiveType(string type)
        {
            for (int i = 0; i < PRIMITIVE_TYPENAMES.Length; i++)
            {
                if (type == PRIMITIVE_TYPENAMES[i]) return true;
            }

            return false;
        }
        private bool IsCustomObject(string typename)
        {
            return typename == TYPENAME_OBJECT;
        }

        private void ValidateAttribute(XElement element, XAttribute attribute, string name, string typename)
        {
            if (attribute == null || string.IsNullOrEmpty(attribute.Value)) throw MetadataParserException.MissingAttribute(element.ToString(), name, typename);
        }

        private MetadataObject ParseObject(IEnumerable<XElement> elements, XElement element)
        {
            // Parse name of the object.
            XAttribute nameAttribute = element.Attribute(NAME_ATTRIB);
            // Check for nulls.
            ValidateAttribute(element, nameAttribute, NAME_ATTRIB, TYPENAME_STRING);
            // Get the name of this object.
            string objectsName = nameAttribute.Value;

            // Parse all fields.
            IEnumerable<XElement> fieldElements = element.Descendants(FIELD_ATTRIB);
            MetadataField[] objectsFields = new MetadataField[fieldElements.Count()];

            // Field index.
            int i = 0;

            foreach (XElement fieldElement in fieldElements)
            {
                // Find field attributes.
                XAttribute fieldNameAttribute = fieldElement.Attribute(NAME_ATTRIB);
                XAttribute fieldTypeAttribute = fieldElement.Attribute(TYPE_ATTRIB);
                XAttribute fieldValueAttribute = fieldElement.Attribute(VALUE_ATTRIB);

                // Check that name and type are not null. Value can be null (default).
                ValidateAttribute(fieldElement, fieldNameAttribute, NAME_ATTRIB, TYPENAME_STRING);
                ValidateAttribute(fieldElement, fieldTypeAttribute, TYPE_ATTRIB, TYPENAME_STRING);

                // Get field values.
                string fieldName = fieldNameAttribute.Value;
                string fieldType = fieldTypeAttribute.Value;
                string fieldStringValue = fieldValueAttribute == null || string.IsNullOrEmpty(fieldValueAttribute.Value) ? string.Empty : fieldValueAttribute.Value;

                // Check if we already have field with same name.
                for (int j = 0; j < i; j++)
                {
                    if (objectsFields[j].Name == fieldName) throw MetadataParserException.FieldNameCollision(objectsName, fieldName);
                }

                // If type is primitive type, just parse it.
                if (IsPrimitiveType(fieldType))
                {
                    // Try to parse a primitive type from the string.
                    // TODO: it should be damn fast but is it clean?

                    object fieldValue = null;

                    // Parse primitive.
                    if (fieldType == TYPENAME_STRING)
                    {
                        // Get string value.
                        fieldValue = fieldStringValue;
                    }
                    if (fieldType == TYPENAME_INT)
                    {
                        // Try to get int value.
                        int num = 0;

                        if (!int.TryParse(fieldStringValue, out num)) throw MetadataParserException.InvalidValue(fieldStringValue, TYPENAME_INT, fieldElement.ToString());

                        fieldValue = num;
                    }
                    else if (fieldType == TYPENAME_BOOL)
                    {
                        // Try to get bool value.
                        bool state = false;

                        if (!bool.TryParse(fieldStringValue, out state)) throw MetadataParserException.InvalidValue(fieldStringValue, TYPENAME_BOOL, fieldElement.ToString());

                        fieldValue = state;
                    }
                    else if (fieldType == TYPENAME_BYTE)
                    {
                        // Try to get byte value.
                        byte num = 0;

                        if (!byte.TryParse(fieldStringValue, out num)) throw MetadataParserException.InvalidValue(fieldStringValue, TYPENAME_BYTE, fieldElement.ToString());

                        fieldValue = num;
                    }
                    else if (fieldType == TYPENAME_FLOAT)
                    {
                        // Try to get float value.
                        float num = 0.0f;

                        if (!float.TryParse(fieldStringValue, out num)) throw MetadataParserException.InvalidValue(fieldStringValue, TYPENAME_FLOAT, fieldElement.ToString());

                        fieldValue = num;
                    }

                    objectsFields[i] = new MetadataField(fieldName, fieldValue);
                }
                else if (IsCustomObject(fieldType))
                {
                    // Check if type is custom object type.
                    IEnumerable<XElement> possibleChilds = elements.Where(e => e.HasAttributes)
                                                                   .Where(e => e.Attribute(NAME_ATTRIB) != null);

                    XElement child = possibleChilds.FirstOrDefault(e => e.Attribute(NAME_ATTRIB).Value == fieldStringValue);

                    // Got child, parse it and set field value to it.
                    if (child != null)
                    {
                        MetadataObject metadataObject = ParseObject(elements, child);

                        objectsFields[i] = new MetadataField(fieldName, metadataObject);
                    }
                    else
                    {
                        // Not a custom type, throw exception.
                        throw new InvalidOperationException("Type must be a primitive or custom type.");
                    }
                }

                i++;
            }

            return new MetadataObject(objectsName, objectsFields);
        }

        private MetadataObject[] ParseObjects(XDocument file)
        {
            IEnumerable<XElement> elements = file.Root.Descendants(OBJECT_DESC);
            MetadataObject[] metadataObjects = new MetadataObject[elements.Count()];

            // Object index.
            int i = 0;

            foreach (XElement element in elements)
            {
                metadataObjects[i] = ParseObject(elements, element);

                // Check for name collisions.
                for (int j = 0; j < i; j++) if (metadataObjects[i].Name == metadataObjects[j].Name) throw MetadataParserException.ObjectNameCollision(metadataObjects[i].Name);
                 
                i++;
            }

            return metadataObjects;
        }

        private string ParseName(XDocument file)
        {
            XAttribute nameAttribute = file.Root.Attribute(NAME_ATTRIB);

            // Check if attribute or the value is null.
            ValidateAttribute(file.Root, nameAttribute, NAME_ATTRIB, TYPENAME_STRING);

            return nameAttribute.Value;
        }
        #endregion

        /// <summary>
        /// Parses a metadata object group from a file.
        /// </summary>
        /// <param name="file">file that contains the group</param>
        /// <returns>group containing the data</returns>
        /// <exception cref="MetadataParserException">Object name collision.</exception>
        /// <exception cref="MetadataParserException">Object field name collision.</exception>
        /// <exception cref="MetadataParserException">Missing attribute.</exception>
        /// <exception cref="MetadataParserException">Invalid value.</exception>
        /// <exception cref="InvalidOperationException">Invalid type.</exception>
        protected override MetadataObjectGroup InternalParse(XDocument file)
        {
            // Get name of the group.
            string name = ParseName(file);

            // Try to parse objects.
            MetadataObject[] objects = ParseObjects(file);

            return new MetadataObjectGroup(name, objects);
        }
    }
}

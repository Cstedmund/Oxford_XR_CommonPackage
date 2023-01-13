#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Reflection;

namespace DTT.ScreenRotationManagement.Utils
{
    /// <summary>
    /// Handles caching the info from the reflection methods, types and properties.
    /// </summary>
    internal class EditorScreenCache
    {
        /// <summary>
        /// Cache for the reflected types.
        /// </summary>
        private Dictionary<string, Type> _typeCache = new Dictionary<string, Type>();

        /// <summary>
        /// Cache for the reflected methods.
        /// </summary>
        private Dictionary<string, MethodInfo> _methodCache = new Dictionary<string, MethodInfo>();

        /// <summary>
        /// Cache for the reflected properties.
        /// </summary>
        private Dictionary<string, PropertyInfo> _propertyCache = new Dictionary<string, PropertyInfo>();

        /// <summary>
        /// Cache for the reflected fields.
        /// </summary>
        private Dictionary<string, FieldInfo> _fieldCache = new Dictionary<string, FieldInfo>();

        /// <summary>
        /// Gets an type from the assembly or the cache if it has already been found.
        /// </summary>
        /// <param name="name">Name of the type.</param>
        /// <param name="assembly">Assembly the type is in.</param>
        /// <returns>The found type.</returns>
        public Type GetType(string name, Assembly assembly)
        {
            Type type = null;
            if (_typeCache.TryGetValue(name, out type))
                return type;

            type = assembly.GetType(name);

            if (type != null)
                _typeCache.Add(name, type);

            return type;
        }

        /// <summary>
        /// Gets a method from a type or the cache if it has already been found.
        /// </summary>
        /// <param name="name">Name of the method.</param>
        /// <param name="type">Type the method is in.</param>
        /// <returns>The found method info.</returns>
        public MethodInfo GetMethod(string name, Type type)
        {
            MethodInfo methodInfo = null;
            if (_methodCache.TryGetValue(name, out methodInfo))
                return methodInfo;

            methodInfo = type.GetMethod(name);

            if (methodInfo != null)
                _methodCache.Add(name, methodInfo);

            return methodInfo;
        }

        /// <summary>
        /// Gets a property from a type or the cache if it has already been found.
        /// </summary>
        /// <param name="name">Name of the property.</param>
        /// <param name="type">Type the property is in.</param>
        /// <returns>The found property info.</returns>
        public PropertyInfo GetProperty(string name, Type type)
        {
            PropertyInfo propertyInfo = null;
            if (_propertyCache.TryGetValue(name, out propertyInfo))
                return propertyInfo;

            propertyInfo = type.GetProperty(name);

            if (propertyInfo != null)
                _propertyCache.Add(name, propertyInfo);

            return propertyInfo;
        }

        /// <summary>
        /// Gets a property from a type or the cache if it has already been found with the given binding flags.
        /// </summary>
        /// <param name="name">Name of the property.</param>
        /// <param name="type">Type the property is in.</param>
        /// <param name="bindingFlags">Given binding flags for the search of properties.</param>
        /// <returns>The found property info.</returns>
        public PropertyInfo GetProperty(string name, Type type, BindingFlags bindingFlags)
        {
            PropertyInfo propertyInfo = null;
            if (_propertyCache.TryGetValue(name, out propertyInfo))
                return propertyInfo;

            propertyInfo = type.GetProperty(name, bindingFlags);

            if (propertyInfo != null)
                _propertyCache.Add(name, propertyInfo);

            return propertyInfo;
        }

        /// <summary>
        /// Gets a field from a type or the cache if it has already been found with the given binding flags.
        /// </summary>
        /// <param name="name">Name of the field.</param>
        /// <param name="type">Type the field is in.</param>
        /// <param name="bindingFlags">Given binding flags for the search of the field.</param>
        /// <returns>The found field info.</returns>
        public FieldInfo GetField(string name, Type type, BindingFlags bindingFlags)
        {
            FieldInfo fieldInfo = null;
            if (_fieldCache.TryGetValue(name, out fieldInfo))
                return fieldInfo;

            fieldInfo = type.GetField(name, bindingFlags);

            if (fieldInfo != null)
                _fieldCache.Add(name, fieldInfo);

            return fieldInfo;
        }
    }
}

#endif
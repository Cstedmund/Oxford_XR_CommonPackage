#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using DTT.ScreenRotationManagement.Config;

namespace DTT.ScreenRotationManagement.Editor
{
    /// <summary>
    /// Base class for drawing a settings box in the editor.
    /// </summary>
    internal abstract class SettingsSection
    {
        /// <summary>
        /// The minimum field width of the worker property fields.
        /// </summary>
        protected const int MIN_FIELD_WIDTH = 100;

        /// <summary>
        /// Reference to the property cache of the <see cref="ScreenRotationConfig"/>.
        /// </summary>
        protected ScreenRotationConfigPropertyCache PropertyCache => _propertyCache;

        /// <summary>
        /// Instance of the property cache of the <see cref="ScreenRotationConfig"/>.
        /// </summary>
        private ScreenRotationConfigPropertyCache _propertyCache;

        /// <summary>
        /// Name of the section.
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// Sets the name of the editor section.
        /// </summary>
        /// <param name="name"></param>
        public SettingsSection(string name) => _name = name;

        /// <summary>
        /// Draw the content of the settings section.
        /// </summary>
        protected abstract void DrawContent();

        /// <summary>
        /// Draws the box and name of the settings section.
        /// </summary>
        public void Draw()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField(_name, EditorStyles.boldLabel);

            _propertyCache.UpdateRepresentation();

            DrawContent();

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// Sets a reference to the current property cache.
        /// </summary>
        /// <param name="propertyCache"></param>
        public void InitProperties(ScreenRotationConfigPropertyCache propertyCache) => _propertyCache = propertyCache;

        /// <summary>
        /// Draws a property field with a set width.
        /// </summary>
        /// <param name="property">SerializedProperty to be drawn.</param>
        protected void DrawProperty(SerializedProperty property) =>
            EditorGUILayout.PropertyField(property, GUILayout.MinWidth(MIN_FIELD_WIDTH));
    }
}

#endif
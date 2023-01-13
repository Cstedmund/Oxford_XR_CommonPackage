#if UNITY_EDITOR

using DTT.PublishingTools;
using DTT.ScreenRotationManagement.Config;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DTT.ScreenRotationManagement.Editor
{
    /// <summary>
    /// Handles drawing the editor window for the screen rotation management.
    /// </summary>
    [DTTHeader("dtt.screen-rotation-management")]
    internal class ScreenRotationManagementEditor : DTTEditorWindow
    {
        /// <summary>
        /// Minimum width of the editor window.
        /// </summary>
        private const float MIN_WIDTH = 300;

        /// <summary>
        /// Minimum height of the editor window.
        /// </summary>
        private const float MIN_HEIGHT = 100;

        /// <summary>
        /// Serialized object of the <see cref="ScreenRotationConfig"/>.
        /// </summary>
        private SerializedObject _serializedConfig;

        /// <summary>
        /// List of all setting sections active in the editor.
        /// </summary>
        private List<SettingsSection> _sections = new List<SettingsSection>()
        {
            new GeneralSettingsSection(),
            new EditorSettingsSection()
        };

        /// <summary>
        /// Opens the editor window.
        /// </summary>
        [MenuItem("Tools/DTT/Screen Rotation Management/Window")]
        public static void Open() => GetWindow<ScreenRotationManagementEditor>("Screen Rotation Window");

        /// <summary>
        /// Initializes the <see cref="ScreenRotationConfig"/> file of the screen rotation manager.
        /// </summary>
        [InitializeOnLoadMethod]
        private static void OnLoad() => ScreenRotationAssetDatabase.GetOrCreateScreenRotationConfigAsset();

        /// <summary>
        /// Sets the min and max size of the editor window.
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();

            minSize = new Vector2(MIN_WIDTH, MIN_HEIGHT);
        }

        /// <summary>
        /// Draws the GUI of the window.
        /// </summary>
        protected override void OnGUI()
        {
            base.OnGUI();

            if (ScreenRotationAssetDatabase.Config == null)
                return;

            if (_serializedConfig == null)
                InitProperties();

            foreach (SettingsSection section in _sections)
                section.Draw();
        }

        /// <summary>
        /// Initializes the property cache of the <see cref="ScreenRotationConfig"/>.
        /// </summary>
        private void InitProperties()
        {
            _serializedConfig = new SerializedObject(ScreenRotationAssetDatabase.Config);
            ScreenRotationConfigPropertyCache propertyCache = new ScreenRotationConfigPropertyCache(_serializedConfig);

            foreach (SettingsSection section in _sections)
                section.InitProperties(propertyCache);
        }
    }
}

#endif
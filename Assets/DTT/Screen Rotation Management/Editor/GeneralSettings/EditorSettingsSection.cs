#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace DTT.ScreenRotationManagement.Editor
{
    /// <summary>
    /// Draws the editor debug settings for the screen orientation.
    /// </summary>
    internal class EditorSettingsSection : SettingsSection
    {
        /// <summary>
        /// Sets the name of the section.
        /// </summary>
        public EditorSettingsSection() : base("Editor Settings") { }

        /// <summary>
        /// Draws the necessary properties.
        /// </summary>
        protected override void DrawContent()
        {
            EditorGUI.BeginChangeCheck();
            DrawProperty(PropertyCache.rotateInEditor);

            bool canRotateInEditor = EditorApplication.isPlaying &&
                PropertyCache.rotateInEditor.boolValue;

            // Enables settings the orientation in the editor when editor rotation is toggled on
            // and the game is running in play mode.
            GUI.enabled = canRotateInEditor;

            ScreenOrientation displayedOrientation = EditorApplication.isPlaying ?
                ScreenRotationManager.CurrentOrientation :
                (ScreenOrientation)PropertyCache.startOrientation.intValue;

            ScreenOrientation enumIndex = (ScreenOrientation)EditorGUILayout.EnumPopup(
                new GUIContent("Current Orientation",
                    "Displays the current orientation of the game. Change to rotate the screen for testing."),
                displayedOrientation);

            GUI.enabled = true;

            if (EditorGUI.EndChangeCheck())
            {
                PropertyCache.ApplyChanges();

                if (canRotateInEditor)
                    ScreenRotationManager.SetOrientation(enumIndex);
            }
        }
    }
}

#endif
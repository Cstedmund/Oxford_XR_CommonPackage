#if UNITY_EDITOR

using DTT.ScreenRotationManagement.Config;
using UnityEditor;
using UnityEngine;

namespace DTT.ScreenRotationManagement.Editor
{
    /// <summary>
    /// Draws the start settings for the screen orientation of the device.
    /// </summary>
    internal class GeneralSettingsSection : SettingsSection
    {
        /// <summary>
        /// The last selected enum value for the locked orientations field.
        /// Used for when the user locks all orientations, which resets the value to this value.
        /// </summary>
        private int _lastLockedEnumValue = 0;

        /// <summary>
        /// Sets the name of the section.
        /// </summary>
        public GeneralSettingsSection() : base("General Settings") { }

        /// <summary>
        /// Draws the necessary properties.
        /// </summary>
        protected override void DrawContent()
        {
            EditorGUI.BeginChangeCheck();
            DrawProperty(PropertyCache.updateOrientationDelay);

            // Gets the current display version of the start orientation enum.
            UIOrientation displayedOrientation = ScreenOrientationConverter.ScreenOrientationToUIOrientation(
                            (ScreenOrientation)PropertyCache.startOrientation.intValue);

            GUIContent startOrientationContent = new GUIContent("Start Orientation",
                            "The initial rotation the application starts on." +
                            "When set to Auto Rotation, it will choose the rotation the user had when starting the application.");

            // Draws the enum value using the UI version of the orientation enum.
            UIOrientation displayedOrientationProperty = (UIOrientation)EditorGUILayout.EnumPopup(
                        startOrientationContent,
                        displayedOrientation);

            // Sets the enum property field to the start orientation value.
            PropertyCache.startOrientation.intValue =
                (int)ScreenOrientationConverter.UIOrientationToScreenOrientation(displayedOrientationProperty);


            // Only enables the locked rotations when auto rotate is set on start.
            GUI.enabled = PropertyCache.startOrientation.intValue == (int)ScreenOrientation.AutoRotation;
            DrawProperty(PropertyCache.lockedOrientations);
            GUI.enabled = true;

            // Updates the properties when changes where made.
            if (EditorGUI.EndChangeCheck())
            {
                UpdateOrientationSettings();
                PropertyCache.ApplyChanges();
            }
        }

        /// <summary>
        /// Updates the orientation settings in the player settings of Unity.
        /// </summary>
        private void UpdateOrientationSettings()
        {
            // Sets the correct default orientation.
            PlayerSettingsHandler.SetStartOrientation((ScreenOrientation)PropertyCache.startOrientation.intValue);

            if ((ScreenOrientation)PropertyCache.startOrientation.intValue == ScreenOrientation.AutoRotation)
            {
                bool setLockedOrientations =
                    PlayerSettingsHandler.SetLockedOrientations((LockedScreenOrientation)PropertyCache.lockedOrientations.intValue);

                if (!setLockedOrientations)
                    PropertyCache.lockedOrientations.intValue = _lastLockedEnumValue;
                else
                    _lastLockedEnumValue = PropertyCache.lockedOrientations.intValue;
            }
        }
    }
}

#endif
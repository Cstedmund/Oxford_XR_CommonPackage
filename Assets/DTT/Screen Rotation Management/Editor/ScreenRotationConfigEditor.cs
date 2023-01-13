#if UNITY_EDITOR

using DTT.ScreenRotationManagement.Config;
using DTT.PublishingTools;
using UnityEditor;
using UnityEngine;

namespace DTT.ScreenRotationManagement.Editor
{
    /// <summary>
    /// Overrides the standard inspector of the <see cref="ScreenRotationConfig"/> and redirects
    /// the user to the editor window.
    /// </summary>
    [CustomEditor(typeof(ScreenRotationConfig))]
    [DTTHeader("dtt.screen-rotation-management")]
    internal class ScreenRotationConfigEditor : DTTInspector
    {
        /// <summary>
        /// Draws the editor button.
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Open Screen Rotation Management Window"))
                ScreenRotationManagementEditor.Open();
        }
    }
}

#endif
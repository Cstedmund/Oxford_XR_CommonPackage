#if UNITY_EDITOR

using DTT.PublishingTools;
using UnityEditor;

namespace DTT.AnalyticsStandardization.Editor
{
    /// <summary>
    /// Class that handles opening the readme window for the screen rotation management package.
    /// </summary>
    internal class ReadMeOpener
    {
        /// <summary>
        /// Opens the readme for this package.
        /// </summary>
        [MenuItem("Tools/DTT/Screen Rotation Management/ReadMe")]
        private static void OpenReadMe() => DTTEditorConfig.OpenReadMe("dtt.screen-rotation-management");
    }
}

#endif
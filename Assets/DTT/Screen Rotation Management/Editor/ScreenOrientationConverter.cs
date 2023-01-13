#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace DTT.ScreenRotationManagement.Editor
{
    /// <summary>
    /// Handles converting the screen orientation enums.
    /// </summary>
    internal static class ScreenOrientationConverter
    {
        /// <summary>
        /// Converts a <see cref="UIOrientation"/> value to a <see cref="ScreenOrientation"/> value.
        /// </summary>
        /// <param name="orientation">Value to convert.</param>
        /// <returns>Converted value.</returns>
        public static ScreenOrientation UIOrientationToScreenOrientation(UIOrientation orientation)
        {
            switch (orientation)
            {
                case UIOrientation.Portrait:
                    return ScreenOrientation.Portrait;
                case UIOrientation.PortraitUpsideDown:
                    return ScreenOrientation.PortraitUpsideDown;
                case UIOrientation.LandscapeLeft:
                    return ScreenOrientation.LandscapeLeft;
                case UIOrientation.LandscapeRight:
                    return ScreenOrientation.LandscapeRight;
                case UIOrientation.AutoRotation:
                    return ScreenOrientation.AutoRotation;
                default:
                    return ScreenOrientation.Portrait;
            }
        }

        /// <summary>
        /// Converts a <see cref="ScreenOrientation"/> value to a <see cref="UIOrientation"/> value.
        /// </summary>
        /// <param name="orientation">Value to convert.</param>
        /// <returns>Converted value.</returns>
        public static UIOrientation ScreenOrientationToUIOrientation(ScreenOrientation orientation)
        {
            switch (orientation)
            {
                case ScreenOrientation.Portrait:
                    return UIOrientation.Portrait;
                case ScreenOrientation.PortraitUpsideDown:
                    return UIOrientation.PortraitUpsideDown;
                case ScreenOrientation.LandscapeLeft:
                    return UIOrientation.LandscapeLeft;
                case ScreenOrientation.LandscapeRight:
                    return UIOrientation.LandscapeRight;
                case ScreenOrientation.AutoRotation:
                    return UIOrientation.AutoRotation;
                default:
                    return UIOrientation.Portrait;
            }
        }
    }
}

#endif
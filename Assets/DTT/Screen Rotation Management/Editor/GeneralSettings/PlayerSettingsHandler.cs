#if UNITY_EDITOR

using DTT.ScreenRotationManagement.Config;
using UnityEditor;
using UnityEngine;

namespace DTT.ScreenRotationManagement.Editor
{
    /// <summary>
    /// Handles updating the player settings of Unity.
    /// </summary>
    public static class PlayerSettingsHandler
    {
        /// <summary>
        /// Sets the locked orientations using the flagged <see cref="LockedScreenOrientation"/> enum.
        /// Returns false when all orientations where locked. This isn't allowed, since 1 orientation
        /// must always be possible.
        /// </summary>
        /// <param name="lockedOrientations">To be locked orientations.</param>
        /// <returns>Whether the orientations were locked or not.</returns>
        public static bool SetLockedOrientations(LockedScreenOrientation lockedOrientations)
        {

            // Checks if all rotations are selected.
            // This throws an error message, since one rotation must always be possible.
            if ((int)lockedOrientations == -1)
            {
                Debug.LogWarning("You can't lock all orientations, one must always be possible.");
                return false;
            }

            // Sets the locked auto rotations.
            PlayerSettings.allowedAutorotateToPortrait = !lockedOrientations.HasFlag(LockedScreenOrientation.Portrait);
            PlayerSettings.allowedAutorotateToPortraitUpsideDown = !lockedOrientations.HasFlag(LockedScreenOrientation.PortraitUpsideDown);
            PlayerSettings.allowedAutorotateToLandscapeLeft = !lockedOrientations.HasFlag(LockedScreenOrientation.LandscapeLeft);
            PlayerSettings.allowedAutorotateToLandscapeRight = !lockedOrientations.HasFlag(LockedScreenOrientation.LandscapeRight);

            return true;
        }

        /// <summary>
        /// Sets the start orientation in the player settings of Unity.
        /// </summary>
        /// <param name="startOrientation">To be set start orientation.</param>
        public static void SetStartOrientation(ScreenOrientation startOrientation) =>
            PlayerSettings.defaultInterfaceOrientation = ScreenOrientationConverter.ScreenOrientationToUIOrientation(startOrientation);
    }
}

#endif
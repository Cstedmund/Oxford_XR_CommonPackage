using UnityEngine;

namespace DTT.ScreenRotationManagement.Demo
{
    /// <summary>
    /// Handles debugging the orientation when the screen rotates.
    /// </summary>
    public class OrientationListener : MonoBehaviour
    {
        /// <summary>
        /// Adds event listeners.
        /// </summary>
        private void OnEnable() => ScreenRotationManager.ScreenRotated += OnRotated;

        /// <summary>
        /// Removes event listeners.
        /// </summary>
        private void OnDisable() => ScreenRotationManager.ScreenRotated -= OnRotated;

        /// <summary>
        /// Debug logs the new rotation.
        /// </summary>
        /// <param name="newOrientation">The new orientation.</param>
        private void OnRotated(ScreenOrientation newOrientation)
            => Debug.Log($"The screen rotated! New orientation: {newOrientation}");
    }
}
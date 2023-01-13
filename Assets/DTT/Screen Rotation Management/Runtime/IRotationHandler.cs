using UnityEngine;

namespace DTT.ScreenRotationManagement
{
    /// <summary>
    /// Interface for handling screen rotations.
    /// </summary>
    internal interface IRotationHandler
    {
        /// <summary>
        /// Current screen orientation of the device.
        /// </summary>
        ScreenOrientation CurrentOrientation { get; }

        /// <summary>
        /// Whether the device can currently automatically rotate the screen.
        /// </summary>
        bool AutoRotate { get; }

        /// <summary>
        /// Sets the screen orientation to a given orientation.
        /// </summary>
        /// <param name="orientation">Orientation the screen will be set to.</param>
        void SetOrientation(ScreenOrientation orientation);

        /// <summary>
        /// Turns auto rotation on or off.
        /// </summary>
        /// <param name="autoRotate">Whether the screen should be able to rotate or not.</param>
        void SetAutoRotation(bool autoRotate);

        /// <summary>
        /// Checks whether the screen orientation changed.
        /// </summary>
        /// <returns>Whether the screen orientation changed.</returns>
        bool CheckRotationChange();
    }
}
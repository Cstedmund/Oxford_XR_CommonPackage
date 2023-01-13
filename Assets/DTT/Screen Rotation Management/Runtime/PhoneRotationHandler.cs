using System;
using System.Linq;
using UnityEngine;

namespace DTT.ScreenRotationManagement
{
    /// <summary>
    /// Handles the screen rotations for Android and iOS devices.
    /// </summary>
    internal class PhoneRotationHandler : IRotationHandler
    {
        /// <summary>
        /// Current orientation of the screen.
        /// </summary>
        private ScreenOrientation _currentOrientation;

        /// <summary>
        /// Whether the screen can auto rotate or not.
        /// </summary>
        private bool _autoRotate;

        /// <summary>
        /// Sets the initial current orientation of the screen.
        /// </summary>
        public PhoneRotationHandler() => _currentOrientation = Screen.orientation;

        /// <summary>
        /// Reference to the current orientation of the screen.
        /// </summary>
        public ScreenOrientation CurrentOrientation => _currentOrientation;

        /// <summary>
        /// Whether the screen can currently auto rotate.
        /// </summary>
        public bool AutoRotate => _autoRotate;

        /// <summary>
        /// Checks whether the screen orientation changed compared to the last orientation.
        /// </summary>
        /// <returns>Whether the screen rotated.</returns>
        public bool CheckRotationChange()
        {
            if (_currentOrientation != Screen.orientation)
            {
                _currentOrientation = Screen.orientation;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Turns auto rotation on or off.
        /// </summary>
        /// <param name="autoRotate">Whether the screen should be able to rotate or not.</param>
        public void SetAutoRotation(bool autoRotate)
        {
            _autoRotate = autoRotate;

            if (autoRotate)
                Screen.orientation = ScreenOrientation.AutoRotation;
            else
                Screen.orientation = CurrentOrientation;
        }

        /// <summary>
        /// Sets the screen orientation to the given orientation.
        /// </summary>
        /// <param name="orientation">Orientation the screen will be set to.</param>
        public void SetOrientation(ScreenOrientation orientation)
        {
            _autoRotate = orientation == ScreenOrientation.AutoRotation;
            Screen.orientation = orientation;
        }
    }
}
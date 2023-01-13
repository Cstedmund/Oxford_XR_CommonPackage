#if UNITY_EDITOR

using DTT.ScreenRotationManagement.Config;
using DTT.ScreenRotationManagement.Utils;
using UnityEngine;

namespace DTT.ScreenRotationManagement
{
    /// <summary>
    /// Handles the screen rotations when playing in the Unity editor.
    /// </summary>
    internal class EditorRotationHandler : IRotationHandler
    {
        /// <summary>
        /// Reference to the current orientation of the screen.
        /// </summary>
        public ScreenOrientation CurrentOrientation => _newOrientation;

        /// <summary>
        /// Whether the screen can currently auto rotate.
        /// </summary>
        public bool AutoRotate => _autoRotate;

        /// <summary>
        /// Newest set orientation of the screen.
        /// </summary>
        private ScreenOrientation _newOrientation;

        /// <summary>
        /// Previous rotation of the screen.
        /// </summary>
        private ScreenOrientation _oldOrientation;

        /// <summary>
        /// Whether the screen can auto rotate or not.
        /// </summary>
        private bool _autoRotate;

        /// <summary>
        /// Reference to the instance of the <see cref="ScreenRotationConfig"/>.
        /// </summary>
        private ScreenRotationConfig _config;

        /// <summary>
        /// Reference to the MonoBehaviour of the worker.
        /// This is used to start a coroutine for setting the correct scale of the editor window.
        /// </summary>
        private MonoBehaviour _workerMonoBehaviour;

        /// <summary>
        /// Sets the initial screen rotation and gets a reference to the config file to access the editor debug settings.
        /// </summary>
        /// <param name="config">Instance of the <see cref="ScreenRotationConfig"/>.</param>
        public EditorRotationHandler(ScreenRotationConfig config, MonoBehaviour worker)
        {
            _workerMonoBehaviour = worker;
            _config = config;

            // Sets the current orientation using the screen size if the start orientation is automatic.
            if (_config.StartOrientation == ScreenOrientation.AutoRotation)
            {
                _oldOrientation = Screen.width > Screen.height ?
                    ScreenOrientation.LandscapeLeft : ScreenOrientation.Portrait;
            }
            else
            {
                _oldOrientation = _config.StartOrientation;
            }

            SetOrientation(_oldOrientation);
        }

        /// <summary>
        /// Checks for an rotation change.
        /// Triggered when the new rotation is different from the old one.
        /// </summary>
        /// <returns>Whether the screen rotated or not.</returns>
        public bool CheckRotationChange()
        {
            if (_newOrientation != _oldOrientation)
            {
                _oldOrientation = _newOrientation;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the screen orientation to the given orientation.
        /// </summary>
        /// <param name="orientation">Orientation the screen will be set to.</param>
        public void SetOrientation(ScreenOrientation orientation)
        {
            _autoRotate = orientation == ScreenOrientation.AutoRotation;

            if (_config.RotateInEditor)
                RotateEditorScreen(orientation);

            _newOrientation = orientation;
        }

        /// <summary>
        /// Turns auto rotation on or off.
        /// </summary>
        /// <param name="autoRotate">Whether the screen should be able to rotate or not.</param>
        public void SetAutoRotation(bool autoRotate) => _autoRotate = autoRotate;

        /// <summary>
        /// Rotates the editor window to the given orientation.
        /// </summary>
        /// <param name="orientation">Orientation the screen will be set to.</param>
        private void RotateEditorScreen(ScreenOrientation orientation)
        {
            Vector2Int currentSize = EditorScreenHandler.GetCurrentSize();

            switch (orientation)
            {
                case ScreenOrientation.Portrait:
                case ScreenOrientation.PortraitUpsideDown:
                    if (_newOrientation != ScreenOrientation.Portrait &&
                        _newOrientation != ScreenOrientation.PortraitUpsideDown &&
                        currentSize.y < currentSize.x)
                        _workerMonoBehaviour?.StartCoroutine(EditorScreenHandler.SetScaleAndSize(currentSize.y, currentSize.x));
                    break;
                case ScreenOrientation.LandscapeLeft:
                case ScreenOrientation.LandscapeRight:
                    if (_newOrientation != ScreenOrientation.LandscapeRight &&
                        _newOrientation != ScreenOrientation.LandscapeLeft &&
                        currentSize.x < currentSize.y)
                        _workerMonoBehaviour?.StartCoroutine(EditorScreenHandler.SetScaleAndSize(currentSize.y, currentSize.x));
                    break;
                default:
                    break;
            }
        }
    }
}

#endif
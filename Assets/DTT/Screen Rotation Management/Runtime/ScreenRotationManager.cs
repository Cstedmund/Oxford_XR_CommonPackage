using System;
using UnityEngine;

namespace DTT.ScreenRotationManagement
{
    /// <summary>
    /// Handles setting and getting screen rotation settings.
    /// </summary>
    public static class ScreenRotationManager
    {
        /// <summary>
        /// The current screen orientation of the device.
        /// </summary>
        public static ScreenOrientation CurrentOrientation => _worker.CurrentOrientation;

        /// <summary>
        /// Whether the screen can currently automatically rotate when tilted.
        /// </summary>
        public static bool AutoRotate
        {
            get => _worker.AutoRotate;
            set => SetAutoRotation(value);
        }

        /// <summary>
        /// Invoked when the screen is rotated.
        /// </summary>
        public static event Action<ScreenOrientation> ScreenRotated
        {
            add => _worker.ScreenRotated += value;
            remove => _worker.ScreenRotated -= value;
        }

        /// <summary>
        /// Reference to the active <see cref="ScreenRotationWorker"/> in the application.
        /// </summary>
        private static ScreenRotationWorker _worker;

        /// <summary>
        /// Sets the orientation of the device using the given orientation.
        /// Note: Setting the orientation disables auto rotation, unless it's set to <see cref="ScreenOrientation.AutoRotation"/>.
        /// </summary>
        /// <param name="orientation">Orientation the screen will be set too.</param>
        public static void SetOrientation(ScreenOrientation orientation) => _worker.SetOrientation(orientation);

        /// <summary>
        /// Toggles the automatic rotation on or off.
        /// </summary>
        /// <param name="autoRotate">Whether the screen is allowed to automatically rotate or not.</param>
        public static void SetAutoRotation(bool autoRotate) => _worker.SetAutoRotation(autoRotate);

        /// <summary>
        /// Allows you to disable the opposite orientations when the screen can auto rotate.
        /// So if you set the orientation to portrait, all landscape orientations will be disabled.
        /// </summary>
        /// <param name="orientation">The base orientation.</param>
        public static void LockOppositeOrientations(ScreenOrientation orientation)
        {
            switch (orientation)
            {
                case ScreenOrientation.LandscapeLeft:
                case ScreenOrientation.LandscapeRight:
                    LockOrientations(ScreenOrientation.Portrait, ScreenOrientation.PortraitUpsideDown);
                    break;
                case ScreenOrientation.Portrait:
                case ScreenOrientation.PortraitUpsideDown:
                    LockOrientations(ScreenOrientation.LandscapeLeft, ScreenOrientation.LandscapeRight);
                    break;
            }
        }

        /// <summary>
        /// Allows you to disable certain orientations when the screen can auto rotate.
        /// The previously locked orientations will be overwritten.
        /// </summary>
        /// <param name="lockedOrientations">Disabled orientations for the screen rotation.</param>
        public static void LockOrientations(params ScreenOrientation[] lockedOrientations) =>
            _worker.LockOrientations(lockedOrientations);

        /// <summary>
        /// Re-enables the locked orientations.
        /// </summary>
        public static void UnLockOrientations() => LockOrientations();

        /// <summary>
        /// Initializes the manager by instantiating a <see cref="ScreenRotationWorker"/>.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            _worker = new GameObject().AddComponent<ScreenRotationWorker>();
            _worker.gameObject.name = nameof(ScreenRotationWorker);
            GameObject.DontDestroyOnLoad(_worker.gameObject);
        }
    }
}
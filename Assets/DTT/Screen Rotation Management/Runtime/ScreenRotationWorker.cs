using DTT.ScreenRotationManagement.Config;
#if UNITY_EDITOR
using DTT.ScreenRotationManagement.Utils;
#endif
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace DTT.ScreenRotationManagement
{
    /// <summary>
    /// Handles rotating the screen and checking for changes in the screens orientation in certain intervals.
    /// </summary>
    public class ScreenRotationWorker : MonoBehaviour
    {
        /// <summary>
        /// Invoked when the screen orientation changes.
        /// </summary>
        public event Action<ScreenOrientation> ScreenRotated;

        /// <summary>
        /// The current orientation of the device.
        /// </summary>
        public ScreenOrientation CurrentOrientation => _rotationHandler.CurrentOrientation;

        /// <summary>
        /// Whether the device can currently automatically rotate the screen.
        /// </summary>
        public bool AutoRotate => _rotationHandler.AutoRotate;

        /// <summary>
        /// Reference to the <see cref="ScreenRotationConfig"/> file of this project.
        /// </summary>
        private static ScreenRotationConfig _config;

        /// <summary>
        /// Handles rotating the screen.
        /// </summary>
        private IRotationHandler _rotationHandler;

        /// <summary>
        /// The initial resolution of the editor game window.
        /// </summary>
        private Vector2Int _initialEditorAspect;

        /// <summary>
        /// The initial scale of the editor game window.
        /// </summary>
        private float _initalEditorScale;

        /// <summary>
        /// Loads the <see cref="ScreenRotationConfig"/> and sets the proper <see cref="IRotationHandler"/>.
        /// </summary>
        private void Awake()
        {
            _config = Resources.Load<ScreenRotationConfig>(nameof(ScreenRotationConfig));

#if UNITY_EDITOR
            _initalEditorScale = EditorScreenHandler.GetCurrentScale();
            _initialEditorAspect = EditorScreenHandler.GetCurrentSize();
            _rotationHandler = new EditorRotationHandler(_config, this);
#else
            _rotationHandler = new PhoneRotationHandler();
#endif
        }

        /// <summary>
        /// Starts checking for changes in the screens orientation.
        /// </summary>
        private void OnEnable() => StartCoroutine(CheckScreenRotation());

        /// <summary>
        /// Checks for the screens orientation on certain intervals.
        /// </summary>
        private IEnumerator CheckScreenRotation()
        {
            do
            {
                if (_rotationHandler.CheckRotationChange())
                    ScreenRotated?.Invoke(_rotationHandler.CurrentOrientation);

                yield return new WaitForSeconds(_config.UpdateOrientationDelay);
            } while (enabled);
        }

        /// <summary>
        /// Sets the orientation of the device using the given orientation.
        /// </summary>
        /// <param name="orientation">Orientation the screen must be set too.</param>
        public void SetOrientation(ScreenOrientation orientation) =>
            _rotationHandler.SetOrientation(orientation);

        /// <summary>
        /// Turns auto rotation on or off.
        /// </summary>
        /// <param name="autoRotate">Whether the screen should be able to rotate or not.</param>
        public void SetAutoRotation(bool autoRotate) =>
             _rotationHandler.SetAutoRotation(autoRotate);

        /// <summary>
        /// Locks certain screen rotations from being used when auto rotating.
        /// </summary>
        /// <param name="lockedOrientations">Current locked orientations.</param>
        public void LockOrientations(params ScreenOrientation[] lockedOrientations)
        {
            Screen.autorotateToPortrait = !lockedOrientations.Contains(ScreenOrientation.Portrait);
            Screen.autorotateToPortraitUpsideDown = !lockedOrientations.Contains(ScreenOrientation.PortraitUpsideDown);
            Screen.autorotateToLandscapeLeft = !(lockedOrientations.Contains(ScreenOrientation.LandscapeLeft) || lockedOrientations.Contains(ScreenOrientation.LandscapeLeft));
            Screen.autorotateToLandscapeRight = !lockedOrientations.Contains(ScreenOrientation.LandscapeRight);
        }

#if UNITY_EDITOR
        /// <summary>
        /// Resets the editor screen resolution and scale to the initial resolution.
        /// </summary>
        private void OnApplicationQuit()
        {
            if (!_config.RotateInEditor)
                return;

            EditorScreenHandler.SetScale(_initalEditorScale);
            EditorScreenHandler.SetSize(_initialEditorAspect.x, _initialEditorAspect.y);
        }
#endif
    }
}
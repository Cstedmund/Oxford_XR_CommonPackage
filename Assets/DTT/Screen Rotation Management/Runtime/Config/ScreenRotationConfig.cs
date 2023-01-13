using UnityEngine;

namespace DTT.ScreenRotationManagement.Config
{
    /// <summary>
    /// Contains settings for the screen rotation management asset.
    /// </summary>
    [CreateAssetMenu(menuName = "DTT/Screen Rotation Management/Config", fileName = nameof(ScreenRotationConfig))]
    public class ScreenRotationConfig : ScriptableObject
    {
        /// <summary>
        /// Whether the game window should rotate on screen rotation changes.
        /// </summary>
        [SerializeField]
        [Tooltip("Whether the game window should rotate on screen rotation changes.")]
        private bool _rotateInEditor = true;

        /// <summary>
        /// The interval at which the <see cref="ScreenRotationWorker"/> will check if the screen has rotated.
        /// </summary>
        [SerializeField]
        [Tooltip("The interval at which will be checked if the screen has rotated.")]
        [Min(0)]
        private float _updateOrientationDelay = .2f;

        /// <summary>
        /// The initial rotation the application starts on.
        /// When set to Auto Rotation, it will choose the rotation the user had when starting the application.
        /// </summary>
        [SerializeField]
        [Tooltip("The initial rotation the application starts on." +
            "When set to Auto Rotation, it will choose the rotation the user had when starting the application.")]
        private ScreenOrientation _startOrientation = ScreenOrientation.Portrait;

        /// <summary>
        /// The locked rotations for when the Auto Rotation orientation is selected.
        /// </summary>
        [SerializeField]
        [Tooltip("The locked rotations for when the Auto Rotation orientation is selected.")]
        private LockedScreenOrientation _lockedOrientations;

        /// <summary>
        /// The interval at which the <see cref="ScreenRotationWorker"/> will check if the screen has rotated.
        /// </summary>
        public float UpdateOrientationDelay => _updateOrientationDelay;

        /// <summary>
        /// Whether the game window should rotate on screen rotation changes.
        /// </summary>
        public bool RotateInEditor => _rotateInEditor;

        /// <summary>
        /// The initial rotation the application starts on.
        /// When set to Auto Rotation, it will choose the rotation the user had when starting the application.
        /// </summary>
        public ScreenOrientation StartOrientation => _startOrientation;
    }
}
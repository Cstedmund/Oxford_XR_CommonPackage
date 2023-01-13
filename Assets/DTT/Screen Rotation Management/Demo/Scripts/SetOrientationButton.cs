using UnityEngine;
using UnityEngine.UI;

namespace DTT.ScreenRotationManagement.Demo
{
    /// <summary>
    /// Handles setting the orientation of the screen with a button.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class SetOrientationButton : BaseButton
    {
        /// <summary>
        /// The orientation this button sets the screen too.
        /// </summary>
        [SerializeField]
        private ScreenOrientation _orientation;

        /// <summary>
        /// Standard color the button is set too.
        /// </summary>
        [SerializeField]
        private Color _normalColor;

        /// <summary>
        /// Color of the button when the orientation is equal to this buttons orientation.
        /// </summary>
        [SerializeField]
        private Color _selectedColor;

        /// <summary>
        /// Image component of this object.
        /// </summary>
        private Image _image;

        /// <summary>
        /// Gets the image component and sets the initial color.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            _image = GetComponent<Image>();
            UpdateColor(ScreenRotationManager.CurrentOrientation);
        }

        /// <summary>
        /// Subscribes to the rotation event.
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();
            ScreenRotationManager.ScreenRotated += UpdateColor;
        }

        /// <summary>
        /// Unsubscribes from the rotation event.
        /// </summary>
        protected override void OnDisable()
        {
            base.OnDisable();
            ScreenRotationManager.ScreenRotated -= UpdateColor;
        }

        /// <summary>
        /// Sets the orientation of the screen.
        /// </summary>
        protected override void OnClick() => ScreenRotationManager.SetOrientation(_orientation);

        /// <summary>
        /// Changes the color of the button according to the given orientation.
        /// </summary>
        /// <param name="orientation">New orientation.</param>
        private void UpdateColor(ScreenOrientation orientation)
            => _image.color = orientation == _orientation ? _selectedColor : _normalColor;
    }
}
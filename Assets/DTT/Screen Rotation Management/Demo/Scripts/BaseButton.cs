using UnityEngine;
using UnityEngine.UI;

namespace DTT.ScreenRotationManagement.Demo
{
    /// <summary>
    /// Base class for button behaviour.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public abstract class BaseButton : MonoBehaviour
    {
        /// <summary>
        /// Reference to the button component on this object.
        /// </summary>
        protected Button _button;

        /// <summary>
        /// Gets the button component.
        /// </summary>
        protected virtual void Awake() => _button = GetComponent<Button>();

        /// <summary>
        /// Adds the OnClick method to the click event.
        /// </summary>
        protected virtual void OnEnable() => _button.onClick.AddListener(OnClick);

        /// <summary>
        /// Removes the OnClick method from the click event.
        /// </summary>
        protected virtual void OnDisable() => _button.onClick.RemoveListener(OnClick);

        /// <summary>
        /// Called when the button is clicked.
        /// Implement to add the click behaviour.
        /// </summary>
        protected abstract void OnClick();
    }
}
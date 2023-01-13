using UnityEngine;
using UnityEngine.UI;

namespace DTT.ScreenRotationManagement.Demo
{
    /// <summary>
    /// Displays the current screen orientation.
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class CurrentOrientationText : MonoBehaviour
    {
        /// <summary>
        /// Text component of this object.
        /// </summary>
        private Text _text;

        /// <summary>
        /// Gets the text component.
        /// </summary>
        private void Awake() => _text = GetComponent<Text>();

        /// <summary>
        /// Updates the displayed text.
        /// </summary>
        private void Update() => _text.text = ScreenRotationManager.CurrentOrientation.ToString();
    }
}
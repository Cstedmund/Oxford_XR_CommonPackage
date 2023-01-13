using UnityEngine;

namespace DTT.ScreenRotationManagement.Demo
{
    /// <summary>
    /// This class gives an example on how to implement the 
    /// screen rotation management asset into a project.
    /// </summary>
    public class ScreenRotationBehaviour : MonoBehaviour
    {
        /// <summary>
        /// Sets the orientation settings.
        /// </summary>
        private void Start()
        {
            SetScreenOrientation();
            SetAutoRotation();
            SetRotationListener();
        }

        /// <summary>
        /// Sets the screen orientation to a certain orientation.
        /// </summary>
        private void SetScreenOrientation()
        {
            // Set the orientation of the screen.
            ScreenRotationManager.SetOrientation(ScreenOrientation.LandscapeLeft);

            // Get the current orientation of the screen.
            Debug.Log($"Current orientation: {ScreenRotationManager.CurrentOrientation}");
        }

        /// <summary>
        /// Sets the automatic rotation on and locks certain orientations.
        /// </summary>
        private void SetAutoRotation()
        {
            // Set automatic rotation on.
            ScreenRotationManager.AutoRotate = true;

            // Lock certain orientations from being rotated to.
            ScreenRotationManager.LockOrientations(
                ScreenOrientation.LandscapeRight,
                ScreenOrientation.PortraitUpsideDown
                );
        }

        /// <summary>
        /// Listens to screen rotations.
        /// </summary>
        private void SetRotationListener()
        {
            // Listen to orientation changes.
            ScreenRotationManager.ScreenRotated += orientation =>
            {
                // Stops auto rotating after a certain rotation.
                if (ScreenRotationManager.CurrentOrientation == ScreenOrientation.LandscapeLeft)
                    ScreenRotationManager.AutoRotate = false;
            };
        }
    }
}
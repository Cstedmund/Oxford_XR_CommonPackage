#if TEST_FRAMEWORK

using DTT.ScreenRotationManagement.Config;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace DTT.ScreenRotationManagement.Tests
{
    /// <summary>
    /// Handles tests for the <see cref="ScreenRotationWorker"/>.
    /// </summary>
    public class Test_ScreenRotationWorker
    {
        /// <summary>
        /// Reference to the current <see cref="ScreenRotationWorker"/> in the scene.
        /// </summary>
        private ScreenRotationWorker _worker;

        /// <summary>
        /// Reference to the current <see cref="ScreenRotationConfig"/> file in the project.
        /// </summary>
        private ScreenRotationConfig _config;

        /// <summary>
        /// Finds the current <see cref="ScreenRotationWorker"/> and sets some default values.
        /// </summary>
        [UnitySetUp]
        public IEnumerator Test_Setup()
        {
            _config = Resources.Load<ScreenRotationConfig>(nameof(ScreenRotationConfig));

            yield return null;
            _worker = GameObject.FindObjectOfType<ScreenRotationWorker>();
            _worker.SetOrientation(ScreenOrientation.Portrait);
            yield return new WaitForSeconds(_config.UpdateOrientationDelay + .1f);
        }

        /// <summary>
        /// Expects the orientation to be set to landscape.
        /// </summary>
        [UnityTest]
        public IEnumerator Test_SetOrientation()
        {
            // Arrange.
            ScreenOrientation originalOrientation = _worker.CurrentOrientation;

            // Act.
            _worker.SetOrientation(ScreenOrientation.LandscapeLeft);
            yield return new WaitForSeconds(_config.UpdateOrientationDelay + .1f);

            // Assert.
            Assert.AreNotEqual(originalOrientation, _worker.CurrentOrientation, "The new orientation wasn't successfully set.");
        }

        /// <summary>
        /// Expects auto rotation to be set to true.
        /// </summary>
        [UnityTest]
        public IEnumerator Test_SetAutoOrientation()
        {
            // Arrange.
            yield return null;
            _worker.SetOrientation(ScreenOrientation.Portrait);

            // Act.
            _worker.SetAutoRotation(true);

            // Assert.
            Assert.IsTrue(_worker.AutoRotate);
        }

        /// <summary>
        /// Expects auto rotation to be set to true when setting the orientation to <see cref="ScreenOrientation.AutoRotation"/>.
        /// </summary>
        [UnityTest]
        public IEnumerator Test_SetAutoOrientationEnum()
        {
            // Arrange.
            yield return null;
            _worker.SetOrientation(ScreenOrientation.Portrait);

            // Act.
            _worker.SetOrientation(ScreenOrientation.AutoRotation);

            // Assert.
            Assert.IsTrue(_worker.AutoRotate);
        }
    }
}

#endif
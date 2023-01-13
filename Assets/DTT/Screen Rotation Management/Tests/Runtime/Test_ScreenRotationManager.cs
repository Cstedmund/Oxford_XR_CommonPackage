#if TEST_FRAMEWORK

using DTT.ScreenRotationManagement.Config;
using DTT.ScreenRotationManagement.Editor;
using NUnit.Framework;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace DTT.ScreenRotationManagement.Tests
{
    /// <summary>
    /// Handles test for the <see cref="ScreenRotationManager"/>.
    /// </summary>
    public class Test_ScreenRotationManager
    {
        /// <summary>
        /// Standard orientation used in each test.
        /// </summary>
        private const ScreenOrientation TEST_START_ORIENTATION = ScreenOrientation.Portrait;

        /// <summary>
        /// Standard locked orientations in each test.
        /// Set to 0 to unlock all orientations by default.
        /// </summary>
        private const LockedScreenOrientation TEST_LOCKED_ORIENTATIONS = 0;

        /// <summary>
        /// Holds the original start orientation of the config.
        /// </summary>
        private ScreenOrientation _originalStartOrientation;

        /// <summary>
        /// Holds the original locked orientations of the config.
        /// </summary>
        private LockedScreenOrientation _originalLockedOrientations;

        /// <summary>
        /// Reference to the current <see cref="ScreenRotationConfig"/> file in the project.
        /// </summary>
        private ScreenRotationConfig _config;

        /// <summary>
        /// Serialized version of the current <see cref="ScreenRotationConfig"/>.
        /// </summary>
        private SerializedObject _serializedConfig;

        /// <summary>
        /// Property cache holding each serialized property from the current <see cref="ScreenRotationConfig"/>.
        /// </summary>
        private ScreenRotationConfigPropertyCache _configPropertyCache;

        /// <summary>
        /// Gets the config file and sets up the test values.
        /// </summary>
        [OneTimeSetUp]
        public void Test_OneTimeSetup()
        {
            // Initializes the serialized object and property cache.
            _config = Resources.Load<ScreenRotationConfig>(nameof(ScreenRotationConfig));
            _serializedConfig = new SerializedObject(_config);
            _configPropertyCache = new ScreenRotationConfigPropertyCache(_serializedConfig);

            // Gets the original config values.
            _originalStartOrientation = _config.StartOrientation;
            _originalLockedOrientations = (LockedScreenOrientation)_configPropertyCache.lockedOrientations.intValue;

            // Sets the required test values.
            _configPropertyCache.startOrientation.intValue = (int)TEST_START_ORIENTATION;
            _configPropertyCache.lockedOrientations.intValue = (int)TEST_LOCKED_ORIENTATIONS;
            _configPropertyCache.ApplyChanges();
        }

        /// <summary>
        /// Sets the rotation to the test orientation for testing.
        /// </summary>
        [UnitySetUp]
        public IEnumerator Test_Setup()
        {
            ScreenRotationManager.SetOrientation(TEST_START_ORIENTATION);
            ScreenRotationManager.UnLockOrientations();
            yield return new WaitForSeconds(_config.UpdateOrientationDelay + .1f);
        }

        /// <summary>
        /// Expects the orientation to be set to a new value.
        /// </summary>
        [UnityTest]
        public IEnumerator Test_SetOrientation()
        {
            // Arrange.
            ScreenOrientation oldOrientation = ScreenRotationManager.CurrentOrientation;

            // Act.
            ScreenRotationManager.SetOrientation(ScreenOrientation.PortraitUpsideDown);
            yield return new WaitForSeconds(_config.UpdateOrientationDelay + .1f);

            // Assert.
            Assert.AreNotEqual(oldOrientation, ScreenRotationManager.CurrentOrientation, "The old orientation was equal to the new orientation.");
        }

        /// <summary>
        /// Expects the auto rotation boolean to change to true.
        /// </summary>
        public IEnumerator Test_SetAutoRotation()
        {
            // Arrange.
            bool oldValue = ScreenRotationManager.AutoRotate;

            // Act.
            ScreenRotationManager.AutoRotate = !oldValue;
            yield return null;

            // Assert.
            Assert.IsFalse(oldValue == ScreenRotationManager.AutoRotate, "The value for auto rotation hasn't changed.");
        }

        /// <summary>
        /// Expects the auto rotation to be set to true when the orientation is set too AutoRotation.
        /// </summary>
        [UnityTest]
        public IEnumerator Test_SetAutoOrientationEnum()
        {
            // Act.
            ScreenRotationManager.SetOrientation(ScreenOrientation.AutoRotation);

            // Assert.
            Assert.IsTrue(ScreenRotationManager.AutoRotate, "The auto rotations wasn't set to true after setting the orientation to AutoRotation.");
            yield return null;
        }

        /// <summary>
        /// Expects auto rotation to be disabled after setting the orientation to another orientation.
        /// </summary>
        [UnityTest]
        public IEnumerator Test_DisableAutoOrientation()
        {
            // Arrange.
            ScreenRotationManager.AutoRotate = true;

            // Act.
            ScreenRotationManager.SetOrientation(ScreenOrientation.Portrait);
            yield return null;

            // Assert.
            Assert.IsFalse(ScreenRotationManager.AutoRotate, "The auto rotation wasn't set to false after " +
                "the orientation has been set to another orientation.");
        }

        /// <summary>
        /// Expects the rotation event to be triggered with the correct orientation value.
        /// </summary>
        [UnityTest]
        public IEnumerator Test_ScreenRotatedEvent()
        {
            // Arrange.
            ScreenOrientation oldOrientation = ScreenRotationManager.CurrentOrientation;
            ScreenOrientation newOrientation = oldOrientation;

            void Rotated(ScreenOrientation orientation) => newOrientation = orientation;

            ScreenRotationManager.ScreenRotated += Rotated;

            // Act.
            ScreenRotationManager.SetOrientation(ScreenOrientation.LandscapeRight);
            yield return new WaitForSeconds(_config.UpdateOrientationDelay + .1f);

            // Assert.
            Assert.AreNotEqual(oldOrientation, newOrientation, "The event wasn't triggered.");

            // Cleanup.
            ScreenRotationManager.ScreenRotated -= Rotated;
        }

        /// <summary>
        /// Expects all the orientations to be locked.
        /// </summary>
        [UnityTest]
        public IEnumerator Test_LockOrientations()
        {
            // Act.
            ScreenRotationManager.LockOrientations(ScreenOrientation.Portrait,
                ScreenOrientation.PortraitUpsideDown,
                ScreenOrientation.LandscapeLeft,
                ScreenOrientation.LandscapeRight);

            // Assert.
            Assert.IsFalse(Screen.autorotateToPortraitUpsideDown ||
                Screen.autorotateToPortrait ||
                Screen.autorotateToLandscapeLeft ||
                Screen.autorotateToLandscapeRight,
                "The orientations weren't locked");

            yield return null;
        }

        /// <summary>
        /// Expects the opposite orientations of portrait to be locked.
        /// </summary>
        [UnityTest]
        public IEnumerator Test_LockOppositeOrientationsPortrait()
        {
            // Act.
            ScreenRotationManager.LockOppositeOrientations(ScreenOrientation.Portrait);

            // Assert.
            bool test = Screen.autorotateToPortrait;
            Assert.IsFalse(Screen.autorotateToLandscapeLeft || Screen.autorotateToLandscapeRight, "The opposite orientations of portrait weren't locked");
            yield return null;
        }

        /// <summary>
        /// Expects the opposite orientations of landscape to be locked.
        /// </summary>
        [UnityTest]
        public IEnumerator Test_LockOppositeOrientationsLandscape()
        {
            // Act.
            ScreenRotationManager.LockOppositeOrientations(ScreenOrientation.LandscapeLeft);

            // Assert.
            Assert.IsFalse(Screen.autorotateToPortrait || Screen.autorotateToPortraitUpsideDown, "The opposite orientations of landscape weren't locked");
            yield return null;
        }

        /// <summary>
        /// Sets the config back to its original state.
        /// </summary>
        [OneTimeTearDown]
        public void Test_Cleanup()
        {
            _configPropertyCache.startOrientation.intValue = (int)_originalStartOrientation;
            _configPropertyCache.lockedOrientations.intValue = (int)_originalLockedOrientations;
            _configPropertyCache.ApplyChanges();
        }
    }
}

#endif
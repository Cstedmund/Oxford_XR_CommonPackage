#if TEST_FRAMEWORK

using DTT.ScreenRotationManagement.Config;
using DTT.ScreenRotationManagement.Editor;
using DTT.ScreenRotationManagement.Utils;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace DTT.ScreenRotationManagement.Tests
{
    /// <summary>
    /// Handles tests for the <see cref="EditorRotationHandler"/>.
    /// </summary>
    public class Test_EditorRotationHandler
    {
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
        /// Instance of an <see cref="EditorRotationHandler"/> used for testing.
        /// </summary>
        private EditorRotationHandler _rotationHandler;

        /// <summary>
        /// The original editor game view window size.
        /// Used to reset the resolution to the original size.
        /// </summary>
        private Vector2Int _originalEditorScreenSize;

        private MonoBehaviour _testWorker;

        /// <summary>
        /// Creates a <see cref="ScreenRotationConfig"/> for testing and initializes the <see cref="_rotationHandler"/>
        /// </summary>
        [OneTimeSetUp]
        public void Test_Setup()
        {
            _config = ScriptableObject.CreateInstance<ScreenRotationConfig>();
            _serializedConfig = new SerializedObject(_config);
            _configPropertyCache = new ScreenRotationConfigPropertyCache(_serializedConfig);

            _configPropertyCache.startOrientation.intValue = (int)ScreenOrientation.PortraitUpsideDown;
            _configPropertyCache.ApplyChanges();

            _originalEditorScreenSize = EditorScreenHandler.GetCurrentSize();

            _testWorker = new GameObject().AddComponent<ScreenRotationWorker>();
            _rotationHandler = new EditorRotationHandler(_config, _testWorker);

            // Asserts whether the rotation handler successfully sets up.
            Assert.AreEqual(_rotationHandler.CurrentOrientation, ScreenOrientation.PortraitUpsideDown, "Start orientation wasn't set.");
        }

        /// <summary>
        /// Expects the editor windows size to change to the landscape version of the resolution.
        /// </summary>
        [Test]
        public void Test_SetEditorWindowResolution()
        {
            // Arrange.
            Vector2Int startRes = new Vector2Int(1080, 1920);
            EditorScreenHandler.SetSize(startRes.x, startRes.y);

            // Act.
            _rotationHandler.SetOrientation(ScreenOrientation.LandscapeLeft);

            // Assert.
            Vector2Int endRes = EditorScreenHandler.GetCurrentSize();
            Assert.AreEqual(new Vector2Int(startRes.y, startRes.x), endRes, "The resolution of the editor window wasn't set to the landscape version.");
        }

        /// <summary>
        /// Expects a custom resolution size to be added to the game view.
        /// </summary>
        [Test]
        public void Test_AddCustomSize()
        {
            // Arrange.
            Vector2Int startRes = new Vector2Int(1400, 2400);

            // Act.
            EditorScreenHandler.AddCustomSize(EditorScreenHandler.GameViewSizeType.FIXED, EditorScreenHandler.CurrentSizeGroupType, startRes.x, startRes.y, "TESTRES");

            // Assert.
            int resId = EditorScreenHandler.FindSize(EditorScreenHandler.CurrentSizeGroupType, string.Format("TESTRES", startRes.x, startRes.y));
            Assert.IsTrue(resId != -1,
                "Custom size was not created or found.");

            // Cleanup.
            if (resId != -1)
                EditorScreenHandler.RemoveCustomSize(EditorScreenHandler.CurrentSizeGroupType, resId);
        }

        /// <summary>
        /// Deletes the <see cref="ScreenRotationConfig"/> used for testing and sets the game view resolution back to its original state.
        /// </summary>
        [OneTimeTearDown]
        public void Test_Cleanup()
        {
            ScriptableObject.DestroyImmediate(_config);
            GameObject.Destroy(_testWorker);
            EditorScreenHandler.SetSize(_originalEditorScreenSize.x, _originalEditorScreenSize.y);
        }
    }
}

#endif
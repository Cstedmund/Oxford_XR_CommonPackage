#if UNITY_EDITOR

using System;
using System.Collections;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace DTT.ScreenRotationManagement.Utils
{
    /// <summary>
    /// Handles setting and creating screen resolutions for the editor game window.
    /// </summary>
    internal class EditorScreenHandler
    {
        /// <summary>
        /// Gets the size group type of the current platform.
        /// </summary>
        public static GameViewSizeGroupType CurrentSizeGroupType
        {
            get
            {
                GameViewSizeGroupType gameViewSizeGroupType = GameViewSizeGroupType.Standalone;

#if UNITY_ANDROID
                gameViewSizeGroupType = GameViewSizeGroupType.Android;
#elif UNITY_IOS
                gameViewSizeGroupType = GameViewSizeGroupType.iOS;
#endif
                return gameViewSizeGroupType;
            }
        }

        /// <summary>
        /// Enum used to define whether a Screen size is fixed or aspect.
        /// </summary>
        public enum GameViewSizeType { ASPECT, FIXED }

        /// <summary>
        /// Instance reference to the UnityEditor Game View.
        /// </summary>
        private static object _gameViewSizesInstance;

        /// <summary>
        /// The group within the UnityEditor Game View.
        /// </summary>
        private static MethodInfo _getGroup;

        /// <summary>
        /// Cache holding all used reflections.
        /// </summary>
        private static EditorScreenCache _cache;

        /// <summary>
        /// Constructor for the EditorScreenResolution static class
        /// Finds the references to the required UnityEditor methods.
        /// </summary>
        static EditorScreenHandler()
        {
            _cache = new EditorScreenCache();
            var sizesType = _cache.GetType("UnityEditor.GameViewSizes", typeof(Editor).Assembly);
            var singleType = typeof(ScriptableSingleton<>).MakeGenericType(sizesType);
            var instanceProp = _cache.GetProperty("instance", singleType);
            _getGroup = _cache.GetMethod("GetGroup", sizesType);
            _gameViewSizesInstance = instanceProp.GetValue(null, null);
        }

        /// <summary>
        /// Sets the size and the minimum scale of the editor window.
        /// Creates a new size if one doesn't exist already.
        /// </summary>
        /// <param name="width">Width of the new resolution.</param>
        /// <param name="height">Height of the new resolution.</param>
        public static IEnumerator SetScaleAndSize(int width, int height)
        {
            SetSize(width, height);

            // Waits a frame before updating the scale.
            yield return new WaitForEndOfFrame();
            SetToMinimumScale();
        }

        /// <summary>
        /// Sets the size of the editor window.
        /// Creates a new size if one doesn't exist already.
        /// </summary>
        /// <param name="width">Width of the new resolution.</param>
        /// <param name="height">Height of the new resolution.</param>
        public static void SetSize(int width, int height)
        {
            int foundSize = FindSize(CurrentSizeGroupType, width, height);
            if (foundSize != -1)
            {
                SetSize(foundSize);
            }
            else
            {
                int newSize = AddCustomSize(GameViewSizeType.FIXED, CurrentSizeGroupType,
                        width, height, string.Format("{0}:{1}", width, height));

                if (newSize != -1)
                    SetSize(newSize);
            }
        }

        /// <summary>
        /// Gets the current selected resolution of the Game View.
        /// </summary>
        /// <returns>Current resolution of the game view.</returns>
        public static Vector2Int GetCurrentSize()
        {
            var gvWndType = _cache.GetType("UnityEditor.GameView", typeof(Editor).Assembly);
            var selectedSizeIndexProp = _cache.GetProperty("selectedSizeIndex", gvWndType,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var gvWnd = EditorWindow.GetWindow(gvWndType);
            int sizeIndex = (int)selectedSizeIndexProp.GetValue(gvWnd, null);

            var group = GetGroup(CurrentSizeGroupType);
            var groupType = group.GetType();
            var getGameViewSize = _cache.GetMethod("GetGameViewSize", groupType);
            var gvsType = _cache.GetType("UnityEditor.GameViewSize", typeof(Editor).Assembly);
            var widthProp = _cache.GetProperty("width", gvsType);
            var heightProp = _cache.GetProperty("height", gvsType);
            var indexValue = new object[1];
            indexValue[0] = sizeIndex;
            var size = getGameViewSize.Invoke(group, indexValue);

            int sizeWidth = (int)widthProp.GetValue(size, null);
            int sizeHeight = (int)heightProp.GetValue(size, null);

            return new Vector2Int(sizeWidth, sizeHeight);
        }

        /// <summary>
        /// Method to change the UnityEditor Game View size to the size configured at the provided index.
        /// </summary>
        /// <param name="index">The index of the resolution of which to set.</param>
        public static void SetSize(int index)
        {
            var gvWndType = _cache.GetType("UnityEditor.GameView", typeof(Editor).Assembly);
            var selectedSizeIndexProp = _cache.GetProperty("selectedSizeIndex", gvWndType,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var gvWnd = EditorWindow.GetWindow(gvWndType);
            selectedSizeIndexProp.SetValue(gvWnd, index, null);
        }

        /// <summary>
        /// Add a new custom size to the UnityEditor Game View.
        /// </summary>
        /// <param name="viewSizeType">Whether it's a fixed or aspect ratio resolution <see cref="GameViewSizeType"/>.</param>
        /// <param name="sizeGroupType">The build platform on which to create this new resolution.</param>
        /// <param name="width">The width of the resolution.</param>
        /// <param name="height">The height of the resolution.</param>
        /// <param name="resolutionName">The name of the resolution.</param>
        /// <returns>Returns the index of the newly created resolution.</returns>
        public static int AddCustomSize(GameViewSizeType viewSizeType, GameViewSizeGroupType sizeGroupType, int width, int height, string resolutionName)
        {
            var group = GetGroup(sizeGroupType);
            var addCustomSize = _cache.GetMethod("AddCustomSize", _getGroup.ReturnType);
            var gvsType = _cache.GetType("UnityEditor.GameViewSize", typeof(Editor).Assembly);
            var ctor = gvsType.GetConstructor(new Type[] { _cache.GetType("UnityEditor.GameViewSizeType", typeof(Editor).Assembly),
                typeof(int), typeof(int), typeof(string) });

            var newGvsType = _cache.GetType("UnityEditor.GameViewSizeType", typeof(Editor).Assembly);

            if (viewSizeType == GameViewSizeType.ASPECT)
                newGvsType = _cache.GetType("UnityEditor.GameViewSizeType.AspectRatio", typeof(Editor).Assembly);
            else
                newGvsType = _cache.GetType("UnityEditor.GameViewSizeType.FixedResolution", typeof(Editor).Assembly);

            var newSize = ctor.Invoke(new object[] { newGvsType, width, height, resolutionName });
            addCustomSize.Invoke(group, new object[] { newSize });

            var getDisplayTexts = _cache.GetMethod("GetDisplayTexts", group.GetType());
            var displayTexts = getDisplayTexts.Invoke(group, null) as string[];

            for (int i = 0; i < displayTexts.Length; i++)
            {
                string display = displayTexts[i];

                // The text we get is "Name (W:H)" if the size has a name, or just "W:H" e.g. 16:9.
                int pren = display.IndexOf('(');
                if (pren > 0)
                    display = display.Substring(0, pren - 1);
                if (display == resolutionName)
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Removes a custom resolution from the game view.
        /// </summary>
        /// <param name="sizeGroupType">The build platform on which to create this new resolution.</param>
        /// <param name="id">The ID of the resolution.</param>
        public static void RemoveCustomSize(GameViewSizeGroupType sizeGroupType, int id)
        {
            var group = GetGroup(sizeGroupType);
            var groupType = group.GetType();
            var removeCustomSize = _cache.GetMethod("RemoveCustomSize", groupType);
            removeCustomSize.Invoke(group, new object[] { id });
        }

        /// <summary>
        /// Find a resolution size based on given width and height.
        /// </summary>
        /// <param name="sizeGroupType">The build platform of the resolution to find.</param>
        /// <param name="width">The width of which to match.</param>
        /// <param name="height">The height of which to match.</param>
        /// <returns>Returns the index of the found resolution, returns -1 if none is found.</returns>
        public static int FindSize(GameViewSizeGroupType sizeGroupType, int width, int height)
        {
            var group = GetGroup(sizeGroupType);
            var groupType = group.GetType();
            var getBuiltinCount = _cache.GetMethod("GetBuiltinCount", groupType);
            var getCustomCount = _cache.GetMethod("GetCustomCount", groupType);
            int sizesCount = (int)getBuiltinCount.Invoke(group, null) + (int)getCustomCount.Invoke(group, null);
            var getGameViewSize = _cache.GetMethod("GetGameViewSize", groupType);
            var gvsType = _cache.GetType("UnityEditor.GameViewSize", typeof(Editor).Assembly);
            var widthProp = _cache.GetProperty("width", gvsType);
            var heightProp = _cache.GetProperty("height", gvsType);
            var indexValue = new object[1];

            for (int i = 0; i < sizesCount; i++)
            {
                indexValue[0] = i;
                var size = getGameViewSize.Invoke(group, indexValue);
                int sizeWidth = (int)widthProp.GetValue(size, null);
                int sizeHeight = (int)heightProp.GetValue(size, null);
                if (sizeWidth == width && sizeHeight == height)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Find a resolution size based on the given resolution name.
        /// </summary>
        /// <param name="sizeGroupType">The build platform of the resolution to find.</param>
        /// <param name="resolutionName">The label of the resolution.</param>>
        /// <returns>Returns the index of the found resolution, returns -1 if none is found.</returns>
        public static int FindSize(GameViewSizeGroupType sizeGroupType, string resolutionName)
        {
            var group = GetGroup(sizeGroupType);
            var groupType = group.GetType();
            var getBuiltinCount = _cache.GetMethod("GetBuiltinCount", groupType);
            var getCustomCount = _cache.GetMethod("GetCustomCount", groupType);
            int sizesCount = (int)getBuiltinCount.Invoke(group, null) + (int)getCustomCount.Invoke(group, null);
            var getGameViewSize = _cache.GetMethod("GetGameViewSize", groupType);
            var gvsType = _cache.GetType("UnityEditor.GameViewSize", typeof(Editor).Assembly);
            var textProp = _cache.GetProperty("displayText", gvsType);
            var indexValue = new object[1];

            for (int i = 0; i < sizesCount; i++)
            {
                indexValue[0] = i;
                var size = getGameViewSize.Invoke(group, indexValue);
                string sizeName = (string)textProp.GetValue(size, null);

                int pren = sizeName.IndexOf('(');
                if (pren > 0)
                    sizeName = sizeName.Substring(0, pren - 1);

                if (sizeName == resolutionName)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Sets the minimum possible scale of the current resolution.
        /// </summary>
        public static void SetToMinimumScale()
        {
            Assembly assembly = typeof(EditorWindow).Assembly;
            Type type = _cache.GetType("UnityEditor.GameView", assembly);
            EditorWindow gameViewWindow = EditorWindow.GetWindow(type);

            var areaField = _cache.GetField("m_ZoomArea", type, BindingFlags.Instance | BindingFlags.NonPublic);
            var areaObj = areaField.GetValue(gameViewWindow);

            var minScaleProp = _cache.GetProperty("minScale", type, BindingFlags.Instance | BindingFlags.NonPublic);

            var scaleField = _cache.GetField("m_Scale", areaObj.GetType(), BindingFlags.Instance | BindingFlags.NonPublic);
            float minScale = (float)minScaleProp.GetValue(gameViewWindow);
            scaleField.SetValue(areaObj, new Vector2(minScale, minScale));
        }

        /// <summary>
        /// Gets the current scale of the game view window.
        /// </summary>
        /// <returns>Current scale of the game view window.</returns>
        public static float GetCurrentScale()
        {
            Assembly assembly = typeof(EditorWindow).Assembly;
            Type type = _cache.GetType("UnityEditor.GameView", assembly);
            EditorWindow gameViewWindow = EditorWindow.GetWindow(type);

            var areaField = _cache.GetField("m_ZoomArea", type, BindingFlags.Instance | BindingFlags.NonPublic);
            var areaObj = areaField.GetValue(gameViewWindow);

            var scaleField = _cache.GetField("m_Scale", areaObj.GetType(), BindingFlags.Instance | BindingFlags.NonPublic);
            return ((Vector2)scaleField.GetValue(areaObj)).x;
        }

        /// <summary>
        /// Sets the scale of the game view window.
        /// </summary>
        /// <param name="scale">Scale to be set.</param>
        public static void SetScale(float scale)
        {
            Assembly assembly = typeof(EditorWindow).Assembly;
            Type type = _cache.GetType("UnityEditor.GameView", assembly);
            EditorWindow gameViewWindow = EditorWindow.GetWindow(type);

            var areaField = _cache.GetField("m_ZoomArea", type, BindingFlags.Instance | BindingFlags.NonPublic);
            var areaObj = areaField.GetValue(gameViewWindow);

            var scaleField = _cache.GetField("m_Scale", areaObj.GetType(), BindingFlags.Instance | BindingFlags.NonPublic);
            scaleField.SetValue(areaObj, new Vector2(scale, scale));
        }

        /// <summary>
        /// Get the group of the given Build platform.
        /// </summary>
        /// <param name="type">The build platform.</param>
        /// <returns>An object array of all found resolutions.</returns>
        private static object GetGroup(GameViewSizeGroupType type) =>
            _getGroup.Invoke(_gameViewSizesInstance, new object[] { (int)type });
    }
}

#endif
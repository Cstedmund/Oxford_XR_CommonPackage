#if UNITY_EDITOR

using DTT.Utils.EditorUtilities;
using UnityEditor;
using DTT.ScreenRotationManagement.Config;

namespace DTT.ScreenRotationManagement.Editor
{
    /// <summary>
    /// Provides storage for all the Serialized Properties from the <see cref="ScreenRotationConfig"/>.
    /// </summary>
    internal class ScreenRotationConfigPropertyCache : SerializedPropertyCache
    {
        /// <summary>
        /// Serialized property of the rotate in editor field.
        /// </summary>
        public SerializedProperty rotateInEditor => base[nameof(rotateInEditor)];

        /// <summary>
        /// Serialized property of the update orientation delay field.
        /// </summary>
        public SerializedProperty updateOrientationDelay => base[nameof(updateOrientationDelay)];

        /// <summary>
        /// Serialized property of the start orientation field.
        /// </summary>
        public SerializedProperty startOrientation => base[nameof(startOrientation)];

        /// <summary>
        /// Serialized property of the locked orientations field.
        /// </summary>
        public SerializedProperty lockedOrientations => base[nameof(lockedOrientations)];

        /// <summary>
        /// Constructs the property cache.
        /// </summary>
        /// <param name="serializedObject">Serialized version of a <see cref="ScreenRotationConfig"/>.</param>
        public ScreenRotationConfigPropertyCache(SerializedObject serializedObject) : base(serializedObject) { }
    }
}

#endif
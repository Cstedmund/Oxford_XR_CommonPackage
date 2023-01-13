#if TEST_FRAMEWORK

using DTT.Utils.EditorUtilities;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DTT.ScreenRotationManagement.Tests
{
    /// <summary>
    /// Provides storage for all the Serialized Properties from the <see cref="ScreenRotationWorker"/>.
    /// </summary>
    public class ScreenRotationWorkerPropertyCache : SerializedPropertyCache
    {
        public SerializedProperty config => base[nameof(config)]; 
        public ScreenRotationWorkerPropertyCache(SerializedObject serializedObject) : base(serializedObject) { }
    }
}

#endif
#if UNITY_EDITOR

using DTT.ScreenRotationManagement.Config;
using DTT.PublishingTools;
using System.IO;
using DTT.Utils.EditorUtilities;
using UnityEditor;

namespace DTT.ScreenRotationManagement.Editor
{
    /// <summary>
    /// Handles getting and creating the <see cref="ScreenRotationConfig"/>.
    /// </summary>
    internal static class ScreenRotationAssetDatabase
    {
        /// <summary>
        /// The package name of this package as set in the package.json file.
        /// </summary>
        public static string PACKAGE_NAME = "dtt.screen-rotation-management";

        /// <summary>
        /// Gets the correct directory path for the <see cref="ScreenRotationConfig"/> file.
        /// </summary>
        public static string AssetDirectoryPath
        {
            get
            {
                string packageName = DTTEditorConfig.GetAssetJson(PACKAGE_NAME).displayName;

                return Path.Combine(
                    "Assets",
                    "DTT",
                    packageName,
                    "Resources"
                );
            }
        }

        /// <summary>
        /// Reference to the <see cref="ScreenRotationConfig"/> of this project.
        /// </summary>
        public static ScreenRotationConfig Config { get; private set; }

        /// <summary>
        /// Creates the <see cref="ScreenRotationConfig"/> object inside the resources folder. 
        /// Will return the asset reference if it already exists.
        /// </summary>
        /// <returns>The found/created <see cref="ScreenRotationConfig"/> asset.</returns>
        public static ScreenRotationConfig GetOrCreateScreenRotationConfigAsset()
        {
            // Make sure the default asset path is created.
            string directoryPath = AssetDirectoryPath;

            Directory.CreateDirectory(directoryPath);

            // Create or find the config asset and save it.
            string assetPath = Path.Combine(directoryPath, nameof(ScreenRotationConfig) + ".asset");
            Config = AssetDatabaseUtility.GetOrCreateScriptableObjectAsset<ScreenRotationConfig>(assetPath);

            // Save the asset database changes.
            AssetDatabase.SaveAssets();

            return Config;
        }
    }
}

#endif
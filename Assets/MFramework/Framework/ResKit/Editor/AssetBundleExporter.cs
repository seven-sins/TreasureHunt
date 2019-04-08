using System.IO;
using UnityEngine;

namespace MFramework
{
    public class AssetBundleExporter :MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MFramework/ResKit/Build AssetBundles", false, 1)]
        static void BuildAssetBundles()
        {
            string outputPath = Application.streamingAssetsPath + "/AssetBundles/" + ResKitUtil.GetPlatformName();
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            UnityEditor.BuildPipeline.BuildAssetBundles(outputPath,
                UnityEditor.BuildAssetBundleOptions.ChunkBasedCompression,
                UnityEditor.EditorUserBuildSettings.activeBuildTarget);
            UnityEditor.AssetDatabase.Refresh();
        }

        static string GetPlatformName()
        {
            switch (UnityEditor.EditorUserBuildSettings.activeBuildTarget)
            {
                case UnityEditor.BuildTarget.StandaloneWindows:
                case UnityEditor.BuildTarget.StandaloneWindows64:
                    return "Windows";
                case UnityEditor.BuildTarget.iOS:
                    return "iOS";
                case UnityEditor.BuildTarget.Android:
                    return "Android";
                case UnityEditor.BuildTarget.StandaloneLinux:
                case UnityEditor.BuildTarget.StandaloneLinux64:
                case UnityEditor.BuildTarget.StandaloneLinuxUniversal:
                    return "Linux";
                case UnityEditor.BuildTarget.StandaloneOSX:
                    return "OSX";
                case UnityEditor.BuildTarget.WebGL:
                    return "WebGL";
                default:
                    return null;
            }
        }
#endif
    }

}

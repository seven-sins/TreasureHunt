using System.IO;
using UnityEngine;

namespace MFramework
{
    public class AssetBundleExample : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MFramework/Example/24.AssetBundleExample/Build AssetBundle", false, 24)]
        static void MenuClicked()
        {
            if (!Directory.Exists(Application.streamingAssetsPath))
            {
                Directory.CreateDirectory(Application.streamingAssetsPath);
            }
            UnityEditor.BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath,UnityEditor.BuildAssetBundleOptions.None,UnityEditor.BuildTarget.StandaloneWindows);
        }

        [UnityEditor.MenuItem("MFramework/Example/24.AssetBundleExample/Run", false, 24)]
        static void MenuClicked2()
        {
            UnityEditor.EditorApplication.isPlaying = true;
            new GameObject("AssetBundleExample").AddComponent<AssetBundleExample>();
        }
#endif

        private ResLoader mResLoader = new ResLoader();

        private AssetBundle mAssetBundle;
        void Start()
        {
            mAssetBundle = mResLoader.LoadSync<AssetBundle>("red");
            GameObject gameObject = mAssetBundle.LoadAsset<GameObject>("red");

            Instantiate(gameObject);
        }

        private void OnDestroy()
        {
            mResLoader.Release();
            mResLoader = null;
        }
    }

}

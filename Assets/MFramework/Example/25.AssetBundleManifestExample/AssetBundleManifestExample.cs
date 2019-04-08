using System.Linq;
using UnityEngine;

namespace MFramework
{
    public class AssetBundleManifestExample : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MFramework/Example/25.AssetBundleManifest", false, 25)]
        static void MenuClicked()
        {
            // UnityEditor.EditorApplication.isPlaying = true;
            // new GameObject("AssetBundleExample").AddComponent<AssetBundleExample>();
            AssetBundle assetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/StreamingAssets");
            AssetBundleManifest assetBundleManifest = assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

            assetBundleManifest.GetAllDependencies("red").ToList().ForEach(dependency =>
            {
                Debug.LogFormat("red dependency: {0}", dependency);
            });

            assetBundleManifest.GetAllDependencies("red").ToList().ForEach(item =>
            {
                Debug.Log(item);
            });

            assetBundleManifest.GetDirectDependencies("red").ToList().ForEach(item =>
            {
                Debug.Log(item);
            });

            assetBundle.Unload(true);
        }
#endif

    }

}

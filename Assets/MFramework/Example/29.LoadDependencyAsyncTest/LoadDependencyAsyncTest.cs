using UnityEngine;

namespace MFramework
{
    public class LoadDependencyAsyncTest : MonoBehaviour
    {

#if UNITY_EDITOR
        [UnityEditor.MenuItem("MFramework/Example/29.LoadDependencyAsyncTest", false, 29)]
        static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;
            new GameObject("LoadDependencyAsyncTest").AddComponent<LoadDependencyAsyncTest>();
        }
#endif
        ResLoader mResLoader = new ResLoader();
        private void Start()
        {
            mResLoader.LoadAsync<AssetBundle>("red", bundle =>
            {
                Instantiate(bundle.LoadAsset<GameObject>("red"));
            });
        }

        private void OnDestroy()
        {
            mResLoader.Release();
            mResLoader = null;
        }
    }

}

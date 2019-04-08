using UnityEngine;

namespace MFramework
{
    public class LoadAsyncTest2 : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MFramework/Example/27.LoadAsyncTest", false, 27)]
        static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;
            new GameObject("LoadAsyncTest2").AddComponent<LoadAsyncTest2>();
        }
#endif
        ResLoader mResLoader = new ResLoader();
        private void Start()
        {
            mResLoader.LoadAsync<Texture2D>("resources://BigTexture", bigTexture =>
            {
                Debug.Log(bigTexture.name);
            });
            // mResLoader.LoadSync<AssetBundle>(Application.streamingAssetsPath + "/square");
        }
    }
}



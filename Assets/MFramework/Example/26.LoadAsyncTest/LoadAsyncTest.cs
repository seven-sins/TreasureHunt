using UnityEngine;

namespace MFramework
{
    public class LoadAsyncTest : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MFramework/Example/26.LoadAsyncTest", false, 26)]
        static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;
            new GameObject("LoadAsyncTest").AddComponent<LoadAsyncTest>();
        }
#endif
        ResLoader mResLoader = new ResLoader();
        private void Start()
        {
            mResLoader.LoadAsync<AssetBundle>("square", 
                squareTexture => { Debug.Log(squareTexture.name);});
            mResLoader.LoadSync<AssetBundle>("square");
        }
    }

}


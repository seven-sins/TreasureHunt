using UnityEngine;

namespace MFramework
{
    public class LoadAsyncTest3 : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MFramework/Example/28.LoadAsyncTest", false, 28)]
        static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;
            new GameObject("LoadAsyncTest3").AddComponent<LoadAsyncTest3>();
        }
#endif
        ResLoader mResLoader = new ResLoader();
        private void Start()
        {
            mResLoader.LoadAsync<Texture2D>("resources://BigTexture", bigTexture =>
            {
                Debug.Log(bigTexture.name);
            });
            mResLoader.LoadAsync<Texture2D>("resources://BigTexture", bigTexture =>
            {
                Debug.Log(bigTexture.name);
            });
        }
    }
}

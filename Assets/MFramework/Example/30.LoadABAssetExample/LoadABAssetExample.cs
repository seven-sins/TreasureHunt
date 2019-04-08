using UnityEngine;

namespace MFramework
{
    public class LoadABAssetExample : MonoBehaviour
    {

#if UNITY_EDITOR
        [UnityEditor.MenuItem("MFramework/Example/30.LoadABAsset", false, 30)]
        static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;
            new GameObject("LoadABAssetExample").AddComponent<LoadABAssetExample>();
        }
#endif
        ResLoader mResLoader = new ResLoader();
        private void Start()
        {
            Texture2D squareTexture = mResLoader.LoadSync<Texture2D>("square", "Square");
            Debug.Log(squareTexture.name);

            mResLoader.LoadAsync<GameObject>("red", "Red", gameObjectPrefab =>
            {
                Instantiate(gameObjectPrefab);
            });
        }

        private void OnDestroy()
        {
            mResLoader.Release();
            mResLoader = null;
        }
    }

}

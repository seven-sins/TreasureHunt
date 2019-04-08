using System.Collections;
using UnityEngine;

namespace MFramework
{
    public class UnloadResourcesExample : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MFramework/Example/19.UnloadResourcesExample", false, 19)]
        static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;
            new GameObject("UnloadResourcesExample").AddComponent<UnloadResourcesExample>();
        }
#endif
        IEnumerator Start()
        {
            AudioClip audioClip = Resources.Load<AudioClip>("yu");
            yield return new WaitForSeconds(5.0f);

            Resources.UnloadAsset(audioClip);
            Debug.Log("unload");
        }
    }

}

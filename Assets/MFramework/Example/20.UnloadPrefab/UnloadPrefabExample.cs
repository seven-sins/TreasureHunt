using System.Collections;
using UnityEngine;

namespace MFramework
{
    public class UnloadPrefabExample : MonoBehaviour
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MFramework/Example/20.UnloadPrefabExample", false, 20)]
        static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;
            new GameObject("UnloadPrefabExample").AddComponent<UnloadPrefabExample>();
        }
#endif
        IEnumerator Start()
        {
            //var uiRoot = Resources.Load<GameObject>("UIRoot");
            yield return new WaitForSeconds(5.0f);

            //uiRoot = null;
            //// 该方法有可能会导致各种问题, 不建议使用
            //Resources.UnloadUnusedAssets();
            //Debug.Log("unload");
        }

    }
}


using MFramework;
using UnityEngine;

namespace Game
{
    public class GUIExample : MonoBehaviourSimplify
    {
        protected override void OnBeforeDestroy()
        {

        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("MFramework/Example/12.GUIManager", false, 12)]
        private static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;
            new GameObject("GUIExample").AddComponent<GUIExample>();
        }
#endif
        void Start()
        {
            GUIManager.SetResolution(1280, 720, 0);

            GUIManager.LoadPanel("HomePanel", UILayer.Common);

            Delay(3.0f, () => { GUIManager.UnLoadPanel("HomePanel"); });
        }

    }

}

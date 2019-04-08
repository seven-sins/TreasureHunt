using UnityEngine;

namespace MFramework
{
    public class HideExample
    {

#if UNITY_EDITOR
        [UnityEditor.MenuItem("MFramework/Example/7.Hide脚本", false, 7)]
        private static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;
            new GameObject("Hide").AddComponent<Hide>();
        }
#endif
    }

}

using UnityEngine;

namespace MFramework
{
    public class MonoSingletonExample : MonoSingleton<MonoSingletonExample>
    {

#if UNITY_EDITOR
        [UnityEditor.MenuItem("MFramework/Example/17.MonoSingletonExample", false, 17)]
        static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;
        }
#endif

        [RuntimeInitializeOnLoadMethod]
        static void Example()
        {
            MonoSingletonExample initInstance = MonoSingletonExample.Instance;
            initInstance = MonoSingletonExample.Instance;
        }
    }

}

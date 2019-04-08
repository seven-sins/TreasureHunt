using UnityEngine;

namespace MFramework
{
    public class SingletonExample : Singleton<SingletonExample>
    {

        private SingletonExample()
        {
            Debug.Log("singleton Example ctor");
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("MFramework/Example/16.SingletonExample", false, 16)]
#endif
        static void MenuClicked()
        {
            SingletonExample instance = SingletonExample.Instance;

            instance = SingletonExample.Instance;
        }
    }

}

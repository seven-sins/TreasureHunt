using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFramework
{
    public static class StaticThisExtensionExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MFramework/Example/18.StaticThisExtension", false, 18)]
#endif
        static void MenuClicked()
        {
            new object().Test();
        }

        static void Test(this object selfObj)
        {
            Debug.Log(selfObj);
        }

    }

}


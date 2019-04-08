using MFramework;
using UnityEngine;

namespace MFramework
{
    public class ResolutionCheckExample
    {

#if UNITY_EDITOR
        [UnityEditor.MenuItem("MFramework/Example/3.屏幕宽高比判断", false, 3)]
#endif
        private static void MenuClicked()
        {
            Debug.Log(ResolutionCheck.IsPadResolution() ? "是Pad" : "不是Pad");

            Debug.Log(ResolutionCheck.IsPhoneResolution() ? "是Phone" : "不是Phone");

            Debug.Log(ResolutionCheck.IsPhone15Resolution() ? "是4s" : "不是4s");
        }
    }

}

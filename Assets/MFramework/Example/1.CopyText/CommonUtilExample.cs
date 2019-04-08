using UnityEngine;

namespace MFramework
{
    public class CommonUtilExample
    {
#if UNITY_EDITOR
    [UnityEditor.MenuItem("MFramework/Example/1.要复制的文本", false, 1)]
#endif
        private static void MenuClicked()
        {
            CommonUtil.CopyText("要复制的文本");
        }
        
    }

}

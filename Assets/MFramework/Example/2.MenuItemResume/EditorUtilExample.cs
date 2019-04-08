
namespace MFramework
{

    public class EditorUtilExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MFramework/Example/2.MenuItem复用", false, 2)]
        private static void MenuClicked()
        {
            EditorUtil.CallMenuItem("MFramework/Example/1.要复制的文本");
        }
#endif

    }
}

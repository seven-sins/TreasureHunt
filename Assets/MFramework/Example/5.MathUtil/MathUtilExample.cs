using UnityEngine;

namespace MFramework
{
    public class MathUtilExample
    {

#if UNITY_EDITOR
        [UnityEditor.MenuItem("MFramework/Example/5.概率函数", false, 5)]
#endif
        private static void MenuClicked()
        {
            Debug.Log(MathUtil.Percent(50));

            var randomAge = MathUtil.GetRandomValueFrom(new float[] { 1, 2, 3 });
            Debug.Log(randomAge);
        }
    }

}

using UnityEngine;

namespace MFramework
{
    public class GameObjectSimplifyExample
    {

#if UNITY_EDITOR
        [UnityEditor.MenuItem("MFramework/Example/6.GameObject的API简化", false, 6)]
#endif
        private static void MenuClicked()
        {
            GameObject gameObject = new GameObject();
            gameObject.Hide();

            gameObject.transform.Show();
        }
    }

}

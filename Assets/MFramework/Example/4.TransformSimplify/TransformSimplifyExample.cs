using UnityEngine;

namespace MFramework
{
    public class TransformSimplifyExample
    {

#if UNITY_EDITOR
        [UnityEditor.MenuItem("MFramework/Example/4.Transform API简化", false, 4)]
#endif
        private static void MenuClicked()
        {
            GameObject gameObject = new GameObject();
            gameObject.transform.SetLocalPositionX(5.0f);

            gameObject.transform.SetLocalPositionY(5.0f);

            gameObject.transform.SetLocalPositionZ(5.0f);

            Transform transform = new GameObject("transform").transform;
            gameObject.transform.Identity();
            //
            Transform parentTrans = new GameObject("ParentTransform").transform;
            Transform childTrans = new GameObject("ChildTransform").transform;

            gameObject.transform.AddChild(childTrans);
        }
    }

}
 
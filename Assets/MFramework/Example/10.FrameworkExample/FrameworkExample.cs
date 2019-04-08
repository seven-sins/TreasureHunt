using System.Collections;
using UnityEngine;

namespace MFramework
{
    public class FrameworkExample : MonoBehaviourSimplify
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MFramework/Example/10.框架示例", false, 10)]
        private static void MenuClicked()
        {
            UnityEditor.EditorApplication.isPlaying = true;
            new GameObject("MsgReceiverObj").AddComponent<FrameworkExample>();
        }
#endif
        private void Awake()
        {
            UnRegister("DO");
            UnRegister("DO1");
            UnRegister("DO2");
            UnRegister("DO3");

            Register("DO", data => {
                Debug.Log(data);
            });
            Register("DO1", _ => { });
            Register("DO2", _ => { });
            Register("DO3", _ => { });
        }

        private IEnumerator Start()
        {
            Send("DO", "Hello!");
            yield return new WaitForSeconds(1.0f);

            Send("DO", "Hello~");
        }

        protected override void OnBeforeDestroy()
        {

        }
    }

}


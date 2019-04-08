using UnityEngine;

namespace MFramework
{
    public class MsgDispatcherExample
    {
       
#if UNITY_EDITOR
        [UnityEditor.MenuItem("MFramework/Example/9.简易消息机制", false, 9)]
#endif
        private static void MenuClicked()
        {
            MsgDispatcher.UnRegister("消息1");

            MsgDispatcher.Register("消息1", OnMsgReceived);
            MsgDispatcher.Register("消息1", OnMsgReceived);

            MsgDispatcher.Send("消息1", "Hello World");

            MsgDispatcher.UnRegister("消息1", OnMsgReceived);

            MsgDispatcher.Send("消息1", "Hello");
        }

        private static void OnMsgReceived(object data)
        {
            Debug.LogFormat("消息1:{0}", data);
        }
    }

}

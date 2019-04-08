using System;
using System.Collections.Generic;

namespace MFramework
{
    public class MsgDispatcher 
    {
        static Dictionary<string, Action<object>> mRegistedMsgs = new Dictionary<string, Action<object>>();

        public static void Register(string msgName, Action<object> onMsgReceived)
        {
            if (!mRegistedMsgs.ContainsKey(msgName))
            {
                mRegistedMsgs.Add(msgName, _ => { });
            }
            mRegistedMsgs[msgName] += onMsgReceived;
        }

        public static void UnRegister(string msgName, Action<object> onMsgReceived)
        {
            if (mRegistedMsgs.ContainsKey(msgName))
            {
                mRegistedMsgs[msgName] -= onMsgReceived;
            }
        }

        public static void UnRegister(string msgName)
        {
            if (mRegistedMsgs.ContainsKey(msgName))
            {
                mRegistedMsgs.Remove(msgName);
            }
        }

        public static void Send(string msgName, object data)
        {
            if (mRegistedMsgs.ContainsKey(msgName))
            {
                mRegistedMsgs[msgName](data);
            }
        }

    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFramework
{
    public abstract class MonoBehaviourSimplify : MonoBehaviour
    {
        #region Delay
        public void Delay(float seconds, Action onFinished)
        {
            StartCoroutine(DelayCoroutine(seconds, onFinished));
        }
        private IEnumerator DelayCoroutine(float seconds, Action onFinished)
        {
            yield return new WaitForSeconds(seconds);
            onFinished();
        }
        #endregion

        #region MsgDispather
        /// <summary>
        /// 消息记录
        /// </summary>
        List<MsgRecord> mMsgRecorder = new List<MsgRecord>();

        class MsgRecord
        {
            private MsgRecord()
            {

            }

            static Stack<MsgRecord> MsgRecordPool = new Stack<MsgRecord>();

            public static MsgRecord Allocate(string msgName, Action<object> onMsgReceived)
            {
                MsgRecord retRecord = MsgRecordPool.Count > 0 ? MsgRecordPool.Pop() : new MsgRecord();

                retRecord.Name = msgName;
                retRecord.OnMsgReceived = onMsgReceived;

                return retRecord;
            }

            public void Recycle()
            {
                Name = null;

                OnMsgReceived = null;

                MsgRecordPool.Push(this);
            }

            public string Name;

            public Action<object> OnMsgReceived;
        }

        public void Register(string msgName, Action<object> onMsgReceived)
        {
            MsgDispatcher.Register(msgName, onMsgReceived);
            mMsgRecorder.Add(MsgRecord.Allocate(msgName, onMsgReceived));
        }
         
        protected void Send(string msgName, object data)
        {
            MsgDispatcher.Send(msgName, data);
        }
        public void UnRegister(string msgName)
        {
            var selectedRecords = mMsgRecorder.FindAll(record => record.Name == msgName);

            selectedRecords.ForEach(record =>
            {
                MsgDispatcher.UnRegister(record.Name, record.OnMsgReceived);
                record.Recycle();
            });

            selectedRecords.Clear();
        }

        public void UnRegister(string msgName, Action<object> onMsgReceived)
        {
            var selectedRecords = mMsgRecorder.FindAll(record => record.Name == msgName && record.OnMsgReceived == onMsgReceived);

            selectedRecords.ForEach(record =>
            {
                MsgDispatcher.UnRegister(record.Name, record.OnMsgReceived);
                record.Recycle();
            });

            selectedRecords.Clear();
        }

        private void OnDestroy()
        {
            OnBeforeDestroy();

            foreach (MsgRecord msgRecord in mMsgRecorder)
            {
                MsgDispatcher.UnRegister(msgRecord.Name, msgRecord.OnMsgReceived);
                msgRecord.Recycle();
            }

            mMsgRecorder.Clear();
        }

        protected abstract void OnBeforeDestroy();
        #endregion
    }

}


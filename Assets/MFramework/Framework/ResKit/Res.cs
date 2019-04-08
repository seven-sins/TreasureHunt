using System;

namespace MFramework
{
    public enum ResState
    {
        Waiting,
        Loading,
        Loaded
    }
    public abstract class Res : SimpleRC
    {
        public ResState State { get { return mState; } protected set {
                mState = value;
                if(mState == ResState.Loaded)
                {
                    if(mOnLoadedEvent != null)
                    {
                        mOnLoadedEvent.Invoke(this);
                    }
                }
            }
        }

        private ResState mState;

        public UnityEngine.Object Asset { get; protected set; }

        public string Name { get; protected set; }
        // 加载资源路径
        // private string mAssetPath;
        /// <summary>
        /// 同步加载
        /// </summary>
        /// <returns></returns>
        public abstract bool LoadSync();
        /// <summary>
        /// 异步加载
        /// </summary>
        /// <param name="onLoaded"></param>
        public abstract void LoadAsync();

        protected abstract void Release();

        protected override void OnZeroRef()
        {
            Release();
        }

        private event Action<Res> mOnLoadedEvent;

        public void RegisterOnLoadedEvent(Action<Res> onLoaded)
        {
            mOnLoadedEvent += onLoaded;
        }

        public void UnRegisterOnLoadedEvent(Action<Res> onLoaded)
        {
            mOnLoadedEvent -= onLoaded;
        }
    }

}
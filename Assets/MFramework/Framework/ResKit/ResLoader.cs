using System;
using System.Collections.Generic;

namespace MFramework
{
    public class ResLoader
    {
        #region API
        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public T LoadSync<T>(string assetBundleName, string assetName) where T : UnityEngine.Object
        {
            return DoLoadSync<T>(assetName, assetBundleName);
        }
        
        public T LoadSync<T>(string assetName) where T : UnityEngine.Object
        {
            return DoLoadSync<T>(assetName);
        }
        
        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <param name="onLoaded"></param>
        public void LoadAsync<T>(string assetName, Action<T> onLoaded) where T : UnityEngine.Object
        {
            DoLoadAsync<T>(assetName, null, onLoaded);
        }

        public void LoadAsync<T>(string assetBundleName, string assetName, Action<T> onLoaded) where T : UnityEngine.Object
        {
            DoLoadAsync<T>(assetName, assetBundleName, onLoaded);
        }

        public void Release()
        {
            mResRecords.ForEach(loadedAsset => { loadedAsset.Release(); });
            mResRecords.Clear();
        }

        #endregion

        #region private
        private List<Res> mResRecords = new List<Res>();
        /// <summary>
        /// 从本地记录中获取资源
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        private Res GetResFromRecord(string assetName)
        {
            return mResRecords.Find(loadedAsset => loadedAsset.Name == assetName);
        }
        /// <summary>
        /// 从全局资源管理获取
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        private Res GetResFromResManager(string assetName)
        {
            return ResManager.Instance.SharedLoadedRes.Find(loadedAsset => loadedAsset.Name == assetName);
        }
        /// <summary>
        /// 添加到本地记录
        /// </summary>
        /// <param name="resFromResManager"></param>
        private void AddRes2Record(Res resFromResManager)
        {
            mResRecords.Add(resFromResManager);
            resFromResManager.Retain();
        }

        private Res CreateRes(string assetName, string ownerBundle = null)
        {
            Res res = ResFactory.Create(assetName, ownerBundle);

            ResManager.Instance.SharedLoadedRes.Add(res);
            this.AddRes2Record(res);
            return res;
        }

        private Res GetOrCreateRes(string assetName)
        {
            Res res = this.GetResFromRecord(assetName);
            if (res != null)
            {
                return res;
            }
            res = this.GetResFromResManager(assetName);
            if (res != null)
            {
                this.AddRes2Record(res);
                return res;
            }
            return res;
        }

        private void DoLoadAsync<T>(string assetName, string ownerBundleName, Action<T> onLoaded) where T : UnityEngine.Object
        {
            Res res = this.GetOrCreateRes(assetName);

            Action<Res> onResLoaded = null;
            onResLoaded = loadedRes =>
            {
                onLoaded(loadedRes.Asset as T);
                res.UnRegisterOnLoadedEvent(onResLoaded);
            };

            if (res != null)
            {
                if (res.State == ResState.Loading)
                {
                    res.RegisterOnLoadedEvent(onResLoaded);
                }
                else if (res.State == ResState.Loaded)
                {
                    onLoaded(res.Asset as T);
                }
                return;
            }

            res = this.CreateRes(assetName, ownerBundleName);

            res.RegisterOnLoadedEvent(onResLoaded);

            res.LoadAsync();
        }

        private T DoLoadSync<T>(string assetName, string assetBundleName = null) where T : UnityEngine.Object
        {
            Res res = this.GetOrCreateRes(assetName);
            if (res != null)
            {
                if (res.State == ResState.Loading)
                {
                    throw new Exception(string.Format("请不要在异步加载资源{0}时, 同步加载{0}", res.Name));
                }
                else if (res.State == ResState.Loaded)
                {
                    return res.Asset as T;
                }
            }

            res = this.CreateRes(assetName, assetBundleName);
            res.LoadSync();

            return res.Asset as T;
        }

        #endregion
    }

}


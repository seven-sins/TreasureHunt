using System;
using UnityEngine;

namespace MFramework
{
    public class ResourcesRes : Res
    {
        // 加载资源路径
        private string mAssetPath;
        public ResourcesRes(string assetPath)
        {
            mAssetPath = assetPath.Substring("resources://".Length);
            Name = assetPath;
            State = ResState.Waiting;
        }
        /// <summary>
        /// 同步加载
        /// </summary>
        /// <returns></returns>
        public override bool LoadSync()
        {
            State = ResState.Loading;
            Asset = Resources.Load(mAssetPath);
            State = ResState.Loaded;

            return Asset;
        }
        /// <summary>
        /// 异步加载
        /// </summary>
        /// <param name="onLoaded"></param>
        public override void LoadAsync()
        {
            State = ResState.Loading;
            ResourceRequest resRequest = Resources.LoadAsync(mAssetPath);
            resRequest.completed += operation =>
            {
                Asset = resRequest.asset;
                State = ResState.Loaded;
            };
        }

        protected override void Release()
        {
            if (Asset is GameObject)
            {
                Asset = null;
                Resources.UnloadUnusedAssets();
            }
            else
            {
                Resources.UnloadAsset(Asset);
            }
            ResManager.Instance.SharedLoadedRes.Remove(this);
            Asset = null;
        }

  
    }

}


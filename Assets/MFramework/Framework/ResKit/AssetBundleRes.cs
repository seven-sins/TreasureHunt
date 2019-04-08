using System;
using UnityEngine;

namespace MFramework
{
    public class AssetBundleRes : Res
    {
        private static AssetBundleManifest mManifest;

        public static AssetBundleManifest Manifest
        {
            get
            {
                if (!mManifest)
                {
                    AssetBundle mainBundle = AssetBundle.LoadFromFile(ResKitUtil.FullPathForAssetBundle(ResKitUtil.GetPlatformName()));
                    mManifest = mainBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                }
                return mManifest;
            }
        }

        public AssetBundle assetBundle {
            get { return Asset as AssetBundle; }
            set { Asset = value; }
        }
        // 加载资源路径
        private string mAssetPath;
        public AssetBundleRes(string assetName)
        {
            mAssetPath = ResKitUtil.FullPathForAssetBundle(assetName);
            Name = assetName;
            State = ResState.Waiting;
        }

        private ResLoader mResLoader = new ResLoader();

        /// <summary>
        /// 同步加载
        /// </summary>
        /// <returns></returns>
        public override bool LoadSync()
        {
            State = ResState.Loading;
            string[] dependencyBundleNames = Manifest.GetDirectDependencies(Name);
            foreach(string dependencyBundleName in dependencyBundleNames)
            {
                mResLoader.LoadSync<AssetBundle>(dependencyBundleName);
            }
            assetBundle = AssetBundle.LoadFromFile(mAssetPath);
            State = ResState.Loaded;

            return assetBundle;
        }

        private void LoadDependencyBundlesAsync(Action onAllLoaded)
        {
            string[] dependencyBundleNames = Manifest.GetDirectDependencies(Name);
            int loadedCount = 0;
            if (dependencyBundleNames.Length == 0)
            {
                onAllLoaded();
            }
            foreach (string dependencyBundleName in dependencyBundleNames)
            {
                mResLoader.LoadAsync<AssetBundle>(dependencyBundleName,
                    dependenBundle =>
                    {
                        loadedCount++;
                        if(loadedCount == dependencyBundleNames.Length)
                        {
                            onAllLoaded();
                        }
                    });
            }
        }

        /// <summary>
        /// 异步加载
        /// </summary>
        /// <param name="onLoaded"></param>
        public override void LoadAsync()
        {
            State = ResState.Loading;
            LoadDependencyBundlesAsync(() =>
            {
                AssetBundleCreateRequest assetBundleRequest = AssetBundle.LoadFromFileAsync(mAssetPath);
                assetBundleRequest.completed += operation =>
                {
                    assetBundle = assetBundleRequest.assetBundle;
                    State = ResState.Loaded;
                };
            });
        }

        protected override void Release()
        {
            if(assetBundle != null)
            {
                assetBundle.Unload(true);
                Asset = null;

                mResLoader.Release();
                mResLoader = null;
            }
            
            ResManager.Instance.SharedLoadedRes.Remove(this);
        }

    }

}
using UnityEngine;

namespace MFramework
{
    public class MonoSingleton<T> : MonoBehaviourSimplify where T : MonoSingleton<T>
    {
        protected static T mInstance = null;

        public static T Instance
        {
            get
            {
                if(mInstance == null)
                {
                    mInstance = FindObjectOfType<T>();
                    if(FindObjectsOfType<T>().Length > 1)
                    {
                        Debug.LogWarning("More than 1");
                        return mInstance;
                    }

                    if(mInstance == null)
                    {
                        string instanceName = typeof(T).Name;
                        Debug.LogFormat("Instance Name: {0}", instanceName);
                        GameObject instanceObj = GameObject.Find(instanceName);
                        if (!instanceObj)
                        {
                            instanceObj = new GameObject(instanceName);
                        }

                        mInstance = instanceObj.AddComponent<T>();
                        // 保证实例不会被释放
                        DontDestroyOnLoad(instanceObj);

                        Debug.LogFormat("Add New Singleton {0} in Game!", instanceName);
                    }
                    else
                    {
                        Debug.LogFormat("Already Exist: {0}", mInstance.name);
                    }
                }

                return mInstance;
            }
        }

        protected override void OnBeforeDestroy()
        {
            mInstance = null;
        }

    }

}
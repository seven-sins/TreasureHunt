using System;
using System.Reflection;

namespace MFramework
{
    public abstract class Singleton<T> where T : Singleton<T>
    {
        private static T mInstance;

        protected Singleton() { }

        public static T Instance
        {
            get
            {
                if (mInstance == null)
                {
                    ConstructorInfo[] ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
                    ConstructorInfo ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);
                    if(ctor == null)
                    {
                        throw new Exception("Non-public ctor() not found!");
                    }
                    mInstance = ctor.Invoke(null) as T;
                }
                return mInstance;
            }
        }

    }

}


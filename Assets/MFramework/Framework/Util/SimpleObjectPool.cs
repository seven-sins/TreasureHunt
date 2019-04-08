using System;
using System.Collections.Generic;

namespace MFramework
{
    #region 对象池接口
    public interface IPool<T>
    {
        /// <summary>
        /// 分发
        /// </summary>
        /// <returns></returns>
        T Allocate();
        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Recycle(T obj);
  
    }
    #endregion

    #region 对象池工厂
    public interface IObjectFactory<T>
    {
        T Create();
    }

    public class CustomObjectFactory<T> : IObjectFactory<T>
    {
        private Func<T> mFactoryMethod;
        public CustomObjectFactory(Func<T> factoryMethod)
        {
            mFactoryMethod = factoryMethod;
        }
        public T Create()
        {
            return mFactoryMethod();
        }
    }
    #endregion

    #region 对象池抽象类
    public abstract class Pool<T> : IPool<T>
    {
        protected Stack<T> mCacheStack = new Stack<T>();

        protected IObjectFactory<T> mFactory;

        public int CurCount
        {
            get { return mCacheStack.Count; }
        }

        public virtual T Allocate()
        {
            return mCacheStack.Count > 0 ? mCacheStack.Pop() : mFactory.Create();
        }

        public abstract bool Recycle(T obj);
    }
    #endregion

    #region 对象池实现类
    public class SimpleObjectPool<T> : Pool<T>
    {
        Action<T> mResetMethod;

        public SimpleObjectPool(Func<T> factoryMethod, Action<T> resetMethod = null, int initCount = 0)
        {
            mFactory = new CustomObjectFactory<T>(factoryMethod);
            mResetMethod = resetMethod;
            for (int i = 0; i < initCount; i++)
            {
                mCacheStack.Push(mFactory.Create());
            }
        }
        public override bool Recycle(T obj)
        {
            if(mResetMethod != null)
            {
                mResetMethod(obj);
            }
            mCacheStack.Push(obj);

            return true;
        }
    }
    #endregion
}

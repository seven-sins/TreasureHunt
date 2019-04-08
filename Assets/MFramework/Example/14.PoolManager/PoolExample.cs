using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFramework
{
    public class PoolExample
    {
        class Fish { }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("MFramework/Example/14.PoolManager", false, 14)]
#endif
        private static void MenuClicked()
        {
            var fishPool = new SimpleObjectPool<Fish>(() => new Fish(), null, 100);

            Debug.LogFormat("fishPool.CurCount:{0}", fishPool.CurCount);

            var fishOne = fishPool.Allocate();

            Debug.LogFormat("fishPool.CurCount:{0}", fishPool.CurCount);

            fishPool.Recycle(fishOne);

            Debug.LogFormat("fishPool.CurCount:{0}", fishPool.CurCount);

            for (int i = 0; i < 10; i++)
            {
                fishPool.Allocate();
            }
            Debug.LogFormat("fishPool.CurCount:{0}", fishPool.CurCount);
        }


    }

}

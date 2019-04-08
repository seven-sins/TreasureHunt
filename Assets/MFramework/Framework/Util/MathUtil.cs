using UnityEngine;

namespace MFramework
{
    public  class MathUtil
    {
 
        public static bool Percent(int percent)
        {
            return Random.Range(0, 100) < percent;
        }
        /// <summary>
        /// 获取随机值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public static T GetRandomValueFrom<T>(params T[] values)
        {
            return values[UnityEngine.Random.Range(0, values.Length)];
        }
    }

}

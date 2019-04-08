using UnityEngine;

namespace MFramework
{
    public static class TransformExtension
    {
        public static void AddChild(this Transform parentTrans, Transform childTrans)
        {
            childTrans.SetParent(parentTrans);
        }

        public static void Identity(this MonoBehaviour monoBehaviour)
        {
            monoBehaviour.transform.Identity();
        }

        public static void Identity(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;
        }

        public static void SetLocalPositionX(this Transform transform, float x)
        {
            var localPosition = transform.localPosition;
            localPosition.x = x;
            transform.localPosition = localPosition;
        }

        public static void SetLocalPositionY(this Transform transform, float y)
        {
            var localPosition = transform.localPosition;
            localPosition.y = y;
            transform.localPosition = localPosition;
        }

        public static void SetLocalPositionZ(this Transform transform, float z)
        {
            var localPosition = transform.localPosition;
            localPosition.z = z;
            transform.localPosition = localPosition;
        }

        public static void SetLocalPositionXY(this Transform transform, float x, float y)
        {
            var localPosition = transform.localPosition;
            localPosition.x = x;
            localPosition.y = y;
            transform.localPosition = localPosition;
        }

        public static void SetLocalPositionXZ(this Transform transform, float x, float z)
        {
            var localPosition = transform.localPosition;
            localPosition.x = x;
            localPosition.z = z;
            transform.localPosition = localPosition;
        }

        public static void SetLocalPositionYZ(this Transform transform, float y, float z)
        {
            var localPosition = transform.localPosition;
            localPosition.y = y;
            localPosition.z = z;
            transform.localPosition = localPosition;
        }
    }

}


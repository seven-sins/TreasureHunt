using UnityEngine;

namespace MFramework
{
    public class CommonUtil
    {

        public static void CopyText(string text)
        {
            GUIUtility.systemCopyBuffer = text;
        }
    }

}

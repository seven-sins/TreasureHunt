#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace MFramework
{
    public class EditorUtil
    {
#if UNITY_EDITOR
        public static void ExportPackage(string assetPathname, string fileName)
        {
            AssetDatabase.ExportPackage(assetPathname, fileName, ExportPackageOptions.Recurse);
        }

        public static void CallMenuItem(string menuName)
        {
            EditorApplication.ExecuteMenuItem(menuName);
        }
#endif
        public static void OpenInFolder(string path)
        {
            Application.OpenURL("file:///" + path);
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MFramework
{
    public enum UILayer
    {
        Bg,
        Common,
        Top
    }

    public class GUIManager
    {
        private static GameObject mShareUIRoot;

        public static GameObject UIRoot
        {
            get
            {
                if(mShareUIRoot == null)
                {
                    mShareUIRoot = GameObject.Instantiate(Resources.Load<GameObject>("UIRoot"));
                    mShareUIRoot.name = "UIRoot";
                }
                return mShareUIRoot;
            }
        }

        private static Dictionary<string, GameObject> mPanelDict = new Dictionary<string, GameObject>();

        public static void SetResolution(float width, float height, float matchWidthOrHeight)
        {
            var canvasScaler = UIRoot.GetComponent<CanvasScaler>();
            canvasScaler.referenceResolution = new Vector2(width, height);
            canvasScaler.matchWidthOrHeight = matchWidthOrHeight;
        }

        public static void UnLoadPanel(string panelName)
        {
            if (mPanelDict.ContainsKey(panelName))
            {
                Object.Destroy(mPanelDict[panelName]);
            }
        }

        public static GameObject LoadPanel(string panelName, UILayer uiLayer)
        {
            var panelPrefab = Resources.Load<GameObject>(panelName);
            var panel = Object.Instantiate(panelPrefab);
            panel.name = panelName;

            switch (uiLayer)
            {
                case UILayer.Bg:
                    panel.transform.SetParent(UIRoot.transform.Find("Bg"));
                    break;
                case UILayer.Common:
                    panel.transform.SetParent(UIRoot.transform.Find("Common"));
                    break;
                case UILayer.Top:
                    panel.transform.SetParent(UIRoot.transform.Find("Top"));
                    break;
                default:
                    break;
            }

            var panelRectTrans = panel.transform as RectTransform;

            panelRectTrans.offsetMin = Vector2.zero;
            panelRectTrans.offsetMax = Vector2.zero;
            panelRectTrans.anchoredPosition3D = Vector2.zero;
            panelRectTrans.anchorMin = Vector2.zero;
            panelRectTrans.anchorMax = Vector2.one;

            mPanelDict.Add(panelName, panel);

            return panel;
        }
    }

}


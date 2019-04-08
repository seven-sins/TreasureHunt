using MFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager> {

    [Header("元素预制体")]
    [Tooltip("透明背景预制体")]
    public GameObject bgElement;
    [Tooltip("边界预制体,顺序为:\n上、下、左、右、左上、右上、左下、右下")]
    public GameObject[] borderElements;

    [Header("地图设置")]
    public int w;
    public int h;

    private void Start()
    {
        // 创建地图
        CreateMap();
        // 设置摄像机
        ResetCamera();
    }

    private void CreateMap()
    {
        Transform bgHolder = GameObject.Find("ElementHolder/Background").transform;
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                Instantiate(bgElement, new Vector3(i, j, 0), Quaternion.identity, bgHolder);
            }
        }

        for (int i = 0; i < w; i++)
        {
            Instantiate(borderElements[0], new Vector3(i, h + 0.25f, 0), Quaternion.identity, bgHolder);
            Instantiate(borderElements[1], new Vector3(i, -1.25f, 0), Quaternion.identity, bgHolder);
        }

        for (int i = 0; i < h; i++)
        {
            Instantiate(borderElements[2], new Vector3(-1.25f, i, 0), Quaternion.identity, bgHolder);
            Instantiate(borderElements[3], new Vector3(w + 0.25f, i, 0), Quaternion.identity, bgHolder);
        }

        Instantiate(borderElements[4], new Vector3(-1.25f, h + 0.25f, 0), Quaternion.identity, bgHolder);
        Instantiate(borderElements[5], new Vector3(w + 0.25f, h + 0.25f, 0), Quaternion.identity, bgHolder);
        Instantiate(borderElements[6], new Vector3(-1.25f, -1.25f, 0), Quaternion.identity, bgHolder);
        Instantiate(borderElements[7], new Vector3(w + 0.25f, -1.25f, 0), Quaternion.identity, bgHolder);
    }

    private void ResetCamera()
    {
        Camera.main.orthographicSize = (h + 3) / 2f;
        Camera.main.transform.position = new Vector3((w - 1) / 2f, (h - 1) / 2f, -10);
    }
}

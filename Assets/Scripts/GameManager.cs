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
    // 基本元素
    public GameObject baseElement;
    public GameObject flagElement;
    public GameObject errorElement;

    [Header("特效")]
    public GameObject smokeEffect;
    public GameObject UncoveredEffect;

    [Header("图片资源")]
    public Sprite[] coverTileSprites;
    public Sprite[] trapSprites;
    public Sprite[] numberSprites;

    [Header("地图设置")]
    public int w;
    public int h;
    // 最小陷井占比
    [Header("最小陷井占比")]
    public float minTrapProbability;
    [Header("最大陷井占比")]
    public float maxTrapProbability;
    [Header("暴露元素占比")]
    public float uncoveredProbability;

    public BaseElement[,] mapArray;

    private void Start()
    {
        mapArray = new BaseElement[w, h];
        // 创建地图
        CreateMap();
        // 设置摄像机
        ResetCamera();
        // 初始化地图元素
        InitMap();
    }

    private void CreateMap()
    {
        Transform bgHolder = GameObject.Find("ElementHolder/Background").transform;
        Transform elementHolder = GameObject.Find("ElementHolder").transform;
        // 创建元素
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                // 创建背景元素
                Instantiate(bgElement, new Vector3(i, j, 0), Quaternion.identity, bgHolder);
                // 创建可操作的无素并放置到地图元素数组中
                mapArray[i, j] = Instantiate(baseElement, new Vector3(i, j, 0), Quaternion.identity, elementHolder).GetComponent<BaseElement>();
            }
        }

        // 创建边界
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

    // 
    private void ResetCamera()
    {
        Camera.main.orthographicSize = (h + 3) / 2f;
        Camera.main.transform.position = new Vector3((w - 1) / 2f, (h - 1) / 2f, -10);
    }

    /// <summary>
    /// 初始化地图无素
    /// </summary>
    private void InitMap()
    {
        List<int> availableIndex = new List<int>();
        for (int i = 0; i < w * h; i++)
        {
            availableIndex.Add(i);
        }
        // 生成陷井
        GenerateTrap(availableIndex);
        // 生成数字
        GenerateNumber(availableIndex);
    }

    /// <summary>
    /// 生成陷井
    /// </summary>
    /// <param name="availableIndex">未初始化的地图元素索引列表</param>
    private void GenerateTrap(List<int> availableIndex)
    {
        float trapProbability = Random.Range(minTrapProbability, maxTrapProbability);
        int trapNum = (int)(availableIndex.Count * trapProbability);
        for(int i = 0; i < trapNum; i++)
        {
            int tempIndex = availableIndex[Random.Range(0, availableIndex.Count)];
            int x, y;
            GetPosition(tempIndex, out x, out y);
            availableIndex.Remove(tempIndex);
            SetElement(tempIndex, ElementContent.Trap);
        }
    }

    /// <summary>
    /// 生成数字
    /// </summary>
    /// <param name="availableIndex">未初始化的地图元素索引列表</param>
    private void GenerateNumber(List<int> availableIndex)
    {
        foreach(int i in availableIndex)
        {
            SetElement(i, ElementContent.Number);
        }
        availableIndex.Clear();
    }

    /// <summary>
    /// 设置元素类型
    /// </summary>
    /// <param name="index">元素所在位置的一维索引值</param>
    /// <param name="content">需要设置的类型</param>
    /// <returns>设置好的新类型的组件</returns>
    private BaseElement SetElement(int index, ElementContent content)
    {
        int x, y;
        GetPosition(index, out x, out y);
        // 先移除旧的
        GameObject tempGameObject = mapArray[x, y].gameObject;
        Destroy(tempGameObject.GetComponent<BaseElement>());
        // 添加新的游戏物体
        switch (content)
        {
            case ElementContent.Trap:
                mapArray[x, y] = tempGameObject.AddComponent<TrapElement>();
                break;
            case ElementContent.Number:
                mapArray[x, y] = tempGameObject.AddComponent<NumberElement>();
                break;
            default:
                break;
        }
        return mapArray[x, y];
    }

    private void GetPosition(int index, out int x, out int y)
    {
        y = index / w;
        x = index - y * w;
    }

    private int GetIndex(int x, int y)
    {
        return w * y + x;
    }

    /// <summary>
    /// 计算指定位置周边八格的陷井个数
    /// </summary>
    /// <param name="x">元素所在位置的x</param>
    /// <param name="y">元素所在位置的y</param>
    /// <returns>陷井个数</returns>
    public int CountAdjacentTraps(int x, int y)
    {
        int count = 0;
        if (IsSameContent(x, y + 1, ElementContent.Trap)) count++;
        if (IsSameContent(x, y - 1, ElementContent.Trap)) count++;
        if (IsSameContent(x - 1, y, ElementContent.Trap)) count++;
        if (IsSameContent(x + 1, y, ElementContent.Trap)) count++;
        if (IsSameContent(x - 1, y + 1, ElementContent.Trap)) count++;
        if (IsSameContent(x + 1, y + 1, ElementContent.Trap)) count++;
        if (IsSameContent(x - 1, y - 1, ElementContent.Trap)) count++;
        if (IsSameContent(x + 1, y - 1, ElementContent.Trap)) count++;
        return count;
    }

    public bool IsSameContent(int x, int y, ElementContent content)
    {
        if (x >= 0 && x < w && y >= 0 && y < h)
        {
            return mapArray[x, y].elementContent == content;
        }
        return false;
    }

    /// <summary>
    /// 泛洪算法翻开连片的空白区域
    /// </summary>
    /// <param name="x">开始泛洪的元素位置x</param>
    /// <param name="y">开始泛洪的元素位置y</param>
    /// <param name="visited">访问记录列表</param>
    public void FloodFillElement(int x, int y, bool[,] visited)
    {
        // 检测x, y是否在范围内
        if (x >= 0 && x < w && y >= 0 && y < h)
        {
            // 是否访问过
            if (visited[x, y]) return;
            // 是否翻开, 如何翻开
            if (mapArray[x, y].elementType != ElementType.CantCovered)
            {
                ((SingleCoveredElement)mapArray[x, y]).UnCoveredElementSingle();
            }
            else return;
            // 周边有陷井
            if(CountAdjacentTraps(x, y) > 0) return;
            // 将自己标记为访问过
            visited[x, y] = true;
            // 让周边格子递归
            FloodFillElement(x - 1, y, visited);
            FloodFillElement(x + 1, y, visited);
            FloodFillElement(x, y - 1, visited);
            FloodFillElement(x, y + 1, visited);
            FloodFillElement(x - 1, y - 1, visited);
            FloodFillElement(x + 1, y + 1, visited);
            FloodFillElement(x - 1, y + 1, visited);
            FloodFillElement(x + 1, y - 1, visited);
        }
    }

    /// <summary>
    /// 快速翻开周边格子
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void UncoverdAdjacentElements(int x, int y)
    {
        int marked = 0;
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (i >= 0 && i < w && j >= 0 && j < h)
                {
                    if (mapArray[i, j].elementState == ElementState.Marked) marked++;
                    if (mapArray[i, j].elementState == ElementState.Uncovered && mapArray[i, j].elementContent == ElementContent.Trap) marked++;
                }
            }
        }
        if (CountAdjacentTraps(x, y) == marked)
        {
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i >= 0 && i < w && j >= 0 && j < h)
                    {
                        if(mapArray[i,j].elementState != ElementState.Marked)
                        {
                            mapArray[i, j].OnPlayerStand();
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 翻开所有陷井
    /// </summary>
    public void DisplayAllTraps()
    {
        foreach(BaseElement element in mapArray)
        {
            if (element.elementContent == ElementContent.Trap)
            {
                ((TrapElement)element).UnCoveredElementSingle();
            }
            if(element.elementContent != ElementContent.Trap && element.elementState == ElementState.Marked)
            {
                Instantiate(errorElement, element.transform);
            }
        }
    }
}

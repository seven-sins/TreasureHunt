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
    public GameObject GoldEffect;

    [Header("图片资源")]
    public Sprite[] coverTileSprites;
    public Sprite[] trapSprites;
    public Sprite[] numberSprites;
    public Sprite[] toolSprites;
    public Sprite[] goldSprites;
    public Sprite[] bigwallSprites;
    public Sprite[] smallwallSprites;
    public Sprite[] enemySprites;
    public Sprite[] doorSprites;
    public Sprite[] exitSprites;

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
    // 障碍物
    public int standAreaW;
    public int obstacleAreaW;
    [HideInInspector]
    public int obstacleAreaNum;

    public BaseElement[,] mapArray;

    private void Start()
    {
        mapArray = new BaseElement[w, h];

        obstacleAreaNum = (w - (standAreaW + 3)) / obstacleAreaW;
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

        for (int i = 0; i < 10; i++)
        {
            int tempIndex = availableIndex[Random.Range(0, availableIndex.Count)];
            int x, y;
            GetPosition(tempIndex, out x, out y);
            availableIndex.Remove(tempIndex);
            SetElement(tempIndex, ElementContent.Tool);
        }

        for (int i = 0; i < 10; i++)
        {
            int tempIndex = availableIndex[Random.Range(0, availableIndex.Count)];
            int x, y;
            GetPosition(tempIndex, out x, out y);
            availableIndex.Remove(tempIndex);
            SetElement(tempIndex, ElementContent.Gold);
        }

        int standAreaY = Random.Range(1, h - 1);

        // 生成出口
        GenerateExit(availableIndex);
        //
        GenerateObstacleArea(availableIndex);
        // 生成工具
        GenerateTool(availableIndex);
        // 生成金币
        GenerateGold(availableIndex);
        // 生成陷井
        GenerateTrap(standAreaY, availableIndex);
        // 生成数字
        GenerateNumber(availableIndex);

        GenerateStandArea(standAreaY);
    }

    /// <summary>
    /// 生成道具
    /// </summary>
    /// <param name="availableIndex">尚未初始化的地图元素的索引值列表</param>
    private void GenerateTool(List<int> availableIndex)
    {
        for (int i = 0; i < 3; i++)
        {
            int tempIndex = availableIndex[Random.Range(0, availableIndex.Count)];
            availableIndex.Remove(tempIndex);
            ToolElement t = (ToolElement)SetElement(tempIndex, ElementContent.Tool);
            t.toolType = (ToolType)Random.Range(0, 9);
            if (t.isHide == false)
            {
                t.ConfirmSprite();
            }
        }
    }

    /// <summary>
    /// 生成金币
    /// </summary>
    /// <param name="availableIndex">尚未初始化的地图元素的索引值列表</param>
    private void GenerateGold(List<int> availableIndex)
    {
        for (int i = 0; i < obstacleAreaNum * 3; i++)
        {
            int tempIndex = availableIndex[Random.Range(0, availableIndex.Count)];
            availableIndex.Remove(tempIndex);
            GoldElement g = (GoldElement)SetElement(tempIndex, ElementContent.Gold);
            g.goldType = (GoldType)Random.Range(0, 7);
            if (g.isHide == false)
            {
                g.ConfirmSprite();
            }
        }
    }

    private void GenerateExit(List<int> availableIndex)
    {
        float x = w - 1.5f;
        float y = Random.Range(1, h) - 0.5f;
        BaseElement exit = SetElement(GetIndex((int)(x + 0.5), (int)(y - 0.5)), ElementContent.Exit);
        // 设置位置
        exit.transform.position = new Vector3(x, y, 0);
        // 移出1x1单元格碰撞器
        Destroy(exit.GetComponent<BoxCollider2D>());
        // 添加2x2单元格碰撞器
        exit.gameObject.AddComponent<BoxCollider2D>();
        availableIndex.Remove(GetIndex((int)(x + 0.5), (int)(y - 0.5)));
        availableIndex.Remove(GetIndex((int)(x + 0.5), (int)(y + 0.5)));
        availableIndex.Remove(GetIndex((int)(x - 0.5), (int)(y - 0.5)));
        availableIndex.Remove(GetIndex((int)(x - 0.5), (int)(y + 0.5)));
        Destroy(mapArray[(int)(x + 0.5), (int)(y + 0.5)].gameObject);
        Destroy(mapArray[(int)(x - 0.5), (int)(y - 0.5)].gameObject);
        Destroy(mapArray[(int)(x - 0.5), (int)(y + 0.5)].gameObject);
        mapArray[(int)(x + 0.5), (int)(y + 0.5)] = exit;
        mapArray[(int)(x - 0.5), (int)(y - 0.5)] = exit;
        mapArray[(int)(x - 0.5), (int)(y + 0.5)] = exit;
    }

    /// <summary>
    /// 生成障碍物区
    /// </summary>
    /// <param name="availableIndex"></param>
    private void GenerateObstacleArea(List<int> availableIndex)
    {
        for(int i = 0; i < obstacleAreaNum; i++)
        {
            if (Random.value < 0.5f)
            {
                CreateCloseArea(i, availableIndex);
            }
            else
            {
                CreateRandomWall(i, availableIndex);
            }
        }
    }

    /// <summary>
    /// 闭合区域信息结构体
    /// </summary>
    private struct CloseAreaInfo
    {
        public int x, y, sx, ex, sy, ey;
        public int doorType;
        public Vector2 doorPos;
        public int tx, ty;
        public ToolElement t;
        public int gx, gy;
        public GoldElement g;
        public int innerCount, goldNum;
    }

    /// <summary>
    /// 生成开启闭合障碍物区所需的道具
    /// </summary>
    /// <param name="info">闭合区域信息</param>
    /// <param name="availableIndex">要生成的闭合区域信息结构体</param>
    private void CreateCloseAreaTool(CloseAreaInfo info, List<int> availableIndex)
    {
        info.tx = Random.Range(0, info.sx);
        info.ty = Random.Range(0, h);
        for (; !availableIndex.Contains(GetIndex(info.tx, info.ty));)
        {
            info.tx = Random.Range(0, info.sx);
            info.ty = Random.Range(0, h);
        }
        availableIndex.Remove(GetIndex(info.tx, info.ty));
        info.t = (ToolElement)SetElement(GetIndex(info.tx, info.ty), ElementContent.Tool);
        info.t.toolType = (ToolType)info.doorType;
        if (info.t.isHide == false)
        {
            info.t.ConfirmSprite();
        }
    }

    /// <summary>
    /// 生成闭合区域信息
    /// </summary>
    /// <param name="type">闭合区域类型，0：与边界闭合；1：自闭合</param>
    /// <param name="nowArea">闭合区域索引值</param>
    /// <param name="info">要生成的闭合区域信息结构体</param>
    private void CreateCloseAreaInfo(int type, int nowArea, ref CloseAreaInfo info)
    {
        switch (type)
        {
            case 0:
                info.x = Random.Range(3, obstacleAreaW - 2);
                info.y = Random.Range(3, h - 3);
                info.sx = standAreaW + nowArea * obstacleAreaW + 1;
                info.ex = info.sx + info.x;
                info.doorType = Random.Range(4, 8);
                break;
            case 1:
                info.x = Random.Range(3, obstacleAreaW - 2);
                info.y = Random.Range(3, info.x + 1);
                info.sx = standAreaW + nowArea * obstacleAreaW + 1;
                info.ex = info.sx + info.x;
                info.sy = Random.Range(3, h - info.y - 1);
                info.ey = info.sy + info.y;
                info.doorType = (int)ElementContent.BigWall;
                break;
        }
    }

    /// <summary>
    /// 生成U或L闭合障碍物区域的门
    /// </summary>
    /// <param name="info">闭合区域信息</param>
    /// <param name="availableIndex">要生成的闭合区域信息结构体</param>
    private void CreateULShapeAreaDoor(CloseAreaInfo info, List<int> availableIndex)
    {
        availableIndex.Remove(GetIndex((int)info.doorPos.x, (int)info.doorPos.y));
        SetElement(GetIndex((int)info.doorPos.x, (int)info.doorPos.y), (ElementContent)info.doorType);
    }

    /// <summary>
    /// 生成闭合障碍物区内的奖励物品
    /// </summary>
    /// <param name="info">闭合区域信息</param>
    /// <param name="availableIndex">要生成的闭合区域信息结构体</param>
    private void CreateCloseAreaRewards(CloseAreaInfo info, List<int> availableIndex)
    {
        info.innerCount = info.x * info.y;
        info.goldNum = Random.Range(1, Random.value < 0.5f ? info.innerCount + 1 : info.innerCount / 2);
        for (int i = 0; i < info.goldNum; i++)
        {
            info.gy = i / info.x;
            info.gx = i - info.gy * info.x;
            info.gx = info.sx + info.gx + 1;
            info.gy = info.sy + info.gy + 1;
            if (availableIndex.Contains(GetIndex(info.gx, info.gy)))
            {
                availableIndex.Remove(GetIndex(info.gx, info.gy));
                info.g = (GoldElement)SetElement(GetIndex(info.gx, info.gy), ElementContent.Gold);
                info.g.goldType = (GoldType)Random.Range(0, 7);
                if (info.g.isHide == false)
                {
                    info.g.ConfirmSprite();
                }
            }
        }
    }

    /// <summary>
    /// 创建障碍物闭合区
    /// </summary>
    /// <param name="nowArea">当前障碍物区域索引</param>
    /// <param name="availableIndex"></param>
    private void CreateCloseArea(int nowArea, List<int> availableIndex)
    {
        int shape = Random.Range(0, 2);
        CloseAreaInfo info = new CloseAreaInfo();
        switch (shape)
        {
            case 0:
                CreateCloseAreaInfo(0, nowArea, ref info);
                int dir = Random.Range(0, 4);
                switch (dir)
                {
                    case 0:
                        info.doorPos = Random.value < 0.5f ? new Vector2(Random.Range(info.sx + 1, info.ex), info.y) : new Vector2(Random.value < 0.5f ? info.sx : info.ex, Random.Range(info.y, h));
                        CreateULShapeAreaDoor(info, availableIndex);
                        for (int i = h - 1; i > info.y; i--)
                        {
                            if (availableIndex.Contains(GetIndex(info.sx, i)))
                            {
                                availableIndex.Remove(GetIndex(info.sx, i));
                                SetElement(GetIndex(info.sx, i), ElementContent.BigWall);
                            }
                        }
                        for (int i = info.sx; i < info.ex; i++)
                        {
                            if (availableIndex.Contains(GetIndex(i, info.y)))
                            {
                                availableIndex.Remove(GetIndex(i, info.y));
                                SetElement(GetIndex(i, info.y), ElementContent.BigWall);
                            }
                        }
                        for (int i = h - 1; i >= info.y; i--)
                        {
                            if (availableIndex.Contains(GetIndex(info.ex, i)))
                            {
                                availableIndex.Remove(GetIndex(info.ex, i));
                                SetElement(GetIndex(info.ex, i), ElementContent.BigWall);
                            }
                        }
                        info.sy = info.y;
                        info.ey = h - 1;
                        info.y = h - 1 - info.y;
                        CreateCloseAreaRewards(info, availableIndex);
                        break;
                    case 1:
                        info.doorPos = Random.value < 0.5f ? new Vector2(Random.Range(info.sx + 1, info.ex), info.y) : new Vector2(Random.value < 0.5f ? info.sx : info.ex, Random.Range(0, info.y));
                        CreateULShapeAreaDoor(info, availableIndex);
                        for (int i = 0; i < info.y; i++)
                        {
                            if (availableIndex.Contains(GetIndex(info.sx, i)))
                            {
                                availableIndex.Remove(GetIndex(info.sx, i));
                                SetElement(GetIndex(info.sx, i), ElementContent.BigWall);
                            }
                        }
                        for (int i = info.sx; i < info.ex; i++)
                        {
                            if (availableIndex.Contains(GetIndex(i, info.y)))
                            {
                                availableIndex.Remove(GetIndex(i, info.y));
                                SetElement(GetIndex(i, info.y), ElementContent.BigWall);
                            }
                        }
                        for (int i = 0; i <= info.y; i++)
                        {
                            if (availableIndex.Contains(GetIndex(info.ex, i)))
                            {
                                availableIndex.Remove(GetIndex(info.ex, i));
                                SetElement(GetIndex(info.ex, i), ElementContent.BigWall);
                            }
                        }
                        info.sy = 0;
                        info.ey = info.y;
                        CreateCloseAreaRewards(info, availableIndex);
                        break;
                    case 2:
                        info.doorPos = Random.value < 0.5f ? new Vector2(Random.Range(info.sx + 1, info.ex), info.y) : new Vector2(info.sx, Random.Range(info.y, h));
                        CreateULShapeAreaDoor(info, availableIndex);
                        for (int i = h - 1; i > info.y; i--)
                        {
                            if (availableIndex.Contains(GetIndex(info.sx, i)))
                            {
                                availableIndex.Remove(GetIndex(info.sx, i));
                                SetElement(GetIndex(info.sx, i), ElementContent.BigWall);
                            }
                        }
                        for (int i = info.sx; i < info.ex; i++)
                        {
                            if (availableIndex.Contains(GetIndex(i, info.y)))
                            {
                                availableIndex.Remove(GetIndex(i, info.y));
                                SetElement(GetIndex(i, info.y), ElementContent.BigWall);
                            }
                        }
                        for (int i = 0; i <= info.y; i++)
                        {
                            if (availableIndex.Contains(GetIndex(info.ex, i)))
                            {
                                availableIndex.Remove(GetIndex(info.ex, i));
                                SetElement(GetIndex(info.ex, i), ElementContent.BigWall);
                            }
                        }
                        break;
                    case 3:
                        info.doorPos = Random.value < 0.5f ? new Vector2(Random.Range(info.sx + 1, info.ex), info.y) : new Vector2(info.sx, Random.Range(0, info.y));
                        CreateULShapeAreaDoor(info, availableIndex);
                        for (int i = 0; i < info.y; i++)
                        {
                            if (availableIndex.Contains(GetIndex(info.sx, i)))
                            {
                                availableIndex.Remove(GetIndex(info.sx, i));
                                SetElement(GetIndex(info.sx, i), ElementContent.BigWall);
                            }
                        }
                        for (int i = info.sx; i < info.ex; i++)
                        {
                            if (availableIndex.Contains(GetIndex(i, info.y)))
                            {
                                availableIndex.Remove(GetIndex(i, info.y));
                                SetElement(GetIndex(i, info.y), ElementContent.BigWall);
                            }
                        }
                        for (int i = h - 1; i >= info.y; i--)
                        {
                            if (availableIndex.Contains(GetIndex(info.ex, i)))
                            {
                                availableIndex.Remove(GetIndex(info.ex, i));
                                SetElement(GetIndex(info.ex, i), ElementContent.BigWall);
                            }
                        }
                        break;
                }
                CreateCloseAreaTool(info, availableIndex);
                break;
            case 1:
                CreateCloseAreaInfo(1, nowArea, ref info);
                for (int i = info.sx; i <= info.ex; i++)
                {
                    if (availableIndex.Contains(GetIndex(i, info.sy)))
                    {
                        availableIndex.Remove(GetIndex(i, info.sy));
                        SetElement(GetIndex(i, info.sy), ElementContent.BigWall);
                    }
                    if (availableIndex.Contains(GetIndex(i, info.ey)))
                    {
                        availableIndex.Remove(GetIndex(i, info.ey));
                        SetElement(GetIndex(i, info.ey), ElementContent.BigWall);
                    }
                }
                for (int i = info.sy + 1; i < info.ey; i++)
                {
                    if (availableIndex.Contains(GetIndex(info.sx, i)))
                    {
                        availableIndex.Remove(GetIndex(info.sx, i));
                        SetElement(GetIndex(info.sx, i), ElementContent.BigWall);
                    }
                    if (availableIndex.Contains(GetIndex(info.ex, i)))
                    {
                        availableIndex.Remove(GetIndex(info.ex, i));
                        SetElement(GetIndex(info.ex, i), ElementContent.BigWall);
                    }
                }
                CreateCloseAreaTool(info, availableIndex);
                CreateCloseAreaRewards(info, availableIndex);
                break;
        }
    }

    /// <summary>
    /// 生成随机离散障碍物
    /// </summary>
    /// <param name="nowArea">当前障碍物区域索引</param>
    /// <param name="availableIndex"></param>
    private void CreateRandomWall(int nowArea, List<int> availableIndex)
    {
        for (int i = 0; i < 5; i++)
        {
            int sx = standAreaW + nowArea * obstacleAreaW + 1;
            int ex = sx + obstacleAreaW;
            int wx = Random.Range(sx, ex);
            int wy = Random.Range(0, h);
            for (; !availableIndex.Contains(GetIndex(wx, wy));)
            {
                wx = Random.Range(sx, ex);
                wy = Random.Range(0, h);
            }
            availableIndex.Remove(GetIndex(wx, wy));
            SetElement(GetIndex(wx, wy), Random.value < 0.5f ? ElementContent.SmallWall : ElementContent.BigWall);
        }
    }

    /// <summary>
    /// 生成陷井
    /// </summary>
    /// <param name="standAreaY">站立区域中心Y坐标</param>
    /// <param name="availableIndex">未初始化的地图元素索引列表</param>
    private void GenerateTrap(int standAreaY, List<int> availableIndex)
    {
        float trapProbability = Random.Range(minTrapProbability, maxTrapProbability);
        int trapNum = (int)(availableIndex.Count * trapProbability);
        for(int i = 0; i < trapNum; i++)
        {
            int tempIndex = availableIndex[Random.Range(0, availableIndex.Count)];
            int x, y;
            GetPosition(tempIndex, out x, out y);
            if(x >= 0 && x < standAreaW && y >= standAreaY - 1 && y <= standAreaY + 1)
            {
                continue;
            }
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
    /// 生成站立区域
    /// </summary>
    /// <param name="y">站立区域中心y坐标</param>
    private void GenerateStandArea(int y)
    {
        for (int i = 0; i < standAreaW; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                ((SingleCoveredElement)mapArray[i, j]).UnCoveredElementSingle();
            }
        }
        //player.transform.position = new Vector3(1, y, 0);
        //prePos = nowPos = player.transform.position.ToVector3Int();
        mapArray[1, y].OnPlayerStand();
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
            case ElementContent.Tool:
                mapArray[x, y] = tempGameObject.AddComponent<ToolElement>();
                break;
            case ElementContent.Gold:
                mapArray[x, y] = tempGameObject.AddComponent<GoldElement>();
                break;
            case ElementContent.Enemy:
                mapArray[x, y] = tempGameObject.AddComponent<EnemyElement>();
                break;
            case ElementContent.Door:
                mapArray[x, y] = tempGameObject.AddComponent<DoorElement>();
                break;
            case ElementContent.BigWall:
                mapArray[x, y] = tempGameObject.AddComponent<BigWallElement>();
                break;
            case ElementContent.SmallWall:
                mapArray[x, y] = tempGameObject.AddComponent<SmallWallElement>();
                break;
            case ElementContent.Exit:
                mapArray[x, y] = tempGameObject.AddComponent<ExitElement>();
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

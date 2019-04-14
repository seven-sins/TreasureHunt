using UnityEngine;

public class NumberElement : SingleCoveredElement
{

	protected override void Awake()
    {
        base.Awake();
        elementState = ElementState.Covered;
        elementContent = ElementContent.Number;
    }

    public override void OnMiddleMouseButton()
    {
        // 检查周边八格并翻开
        GameManager.Instance.UncoverdAdjacentElements(x, y);
    }

    public override void UnCoveredElementSingle()
    {
        if(elementState == ElementState.Uncovered)
        {
            return;
        }
        RemoveFlag();
        elementState = ElementState.Uncovered;
        // 移除阴影
        ClearShadow();
        // 显示泥土翻开的特效 
        Instantiate(GameManager.Instance.UncoveredEffect, transform);
        // 计算并显示自身数字
        LoadSprite(GameManager.Instance.numberSprites[GameManager.Instance.CountAdjacentTraps(x, y)]);
    }

    public override void OnUncoverd()
    {
        // 泛洪算法翻开周边元素
        GameManager.Instance.FloodFillElement(x, y, new bool[GameManager.Instance.w, GameManager.Instance.h]);
    }

}

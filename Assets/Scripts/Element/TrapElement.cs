using UnityEngine;

public class TrapElement : SingleCoveredElement {

    protected override void Awake()
    {
        base.Awake();
        elementState = ElementState.Covered;
        elementContent = ElementContent.Trap;
    }

    public override void UnCoveredElementSingle()
    {
        if (elementState == ElementState.Uncovered)
        {
            return;
        }
        RemoveFlag();
        elementState = ElementState.Uncovered;
        // 移除阴影
        ClearShadow();
        // 显示泥土翻开的特效  
        Instantiate(GameManager.Instance.UncoveredEffect, transform);

        // 随机加载陷井
        LoadSprite(GameManager.Instance.trapSprites[Random.Range(0, GameManager.Instance.trapSprites.Length)]);
    }

    public override void OnUncoverd()
    {
        // 翻开所有雷
        GameManager.Instance.DisplayAllTraps();
    }
}

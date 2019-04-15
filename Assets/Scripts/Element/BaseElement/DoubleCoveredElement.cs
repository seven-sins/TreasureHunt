using UnityEngine;

public class DoubleCoveredElement : SingleCoveredElement
{
    public bool isHide = true;

    protected override void Awake()
    {
        base.Awake();
        elementType = ElementType.DoubleCovered;
        if (Random.value < GameManager.Instance.uncoveredProbability)
        {
            UnCoveredElementSingle();
        }
    }

    public override void OnPlayerStand()
    {
        switch (elementState)
        {
            case ElementState.Covered:
                if (isHide)
                {
                    UnCoveredElementSingle();
                }
                else
                {
                    UnCoveredElement();
                }
                break;
            case ElementState.Uncovered:
                break;
            case ElementState.Marked:
                if (isHide)
                {
                    RemoveFlag();
                }
                break;
        }
    }

    public override void OnRightMouseButton()
    {
        switch (elementState)
        {
            case ElementState.Covered:
                if (isHide)
                {
                    AddFlag();
                }
                break;
            case ElementState.Uncovered:
                break;
            case ElementState.Marked:
                if (isHide)
                {
                    RemoveFlag();
                }
                break;
        }
    }

    public override void OnMiddleMouseButton()
    {
        GameManager.Instance.UncoverdAdjacentElements(x, y);
    }

    public override void UnCoveredElementSingle()
    {
        if (elementState == ElementState.Uncovered)
        {
            return;
        }
        isHide = false;
        RemoveFlag();
        // 移除阴影
        ClearShadow();
        //
        ConfirmSprite();
    }

    public override void OnUncoverd()
    {
        elementState = ElementState.Uncovered;
        ToNumberElement();
    }

    // 加载随机图片(金币等)
    public virtual void ConfirmSprite()
    {

    }
}

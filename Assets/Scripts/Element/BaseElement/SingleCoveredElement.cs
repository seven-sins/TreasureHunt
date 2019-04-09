using UnityEngine;

public class SingleCoveredElement : BaseElement
{
    protected override void Awake()
    {
        base.Awake();
        // 保存自己的类型
        elementType = ElementType.SingleCovered;
        // 随机加载一张泥土的图片
        LoadSprite(GameManager.Instance.coverTileSprites[Random.Range(0, GameManager.Instance.coverTileSprites.Length)]);
    }

    public override void OnRightMouseButton()
    {
        switch (elementState)
        {
            case ElementState.Covered:
                AddFlag();
                break;
            case ElementState.Uncovered:
                break;
            case ElementState.Marked:
                RemoveFlag();
                break;
        }

    }

    public void UnCoveredElement()
    {
        if(elementState == ElementState.Uncovered)
        {
            return;
        }
        UnCoveredElementSingle();
        OnUncoverd();
    }

    public void UnCoveredElementSingle()
    {

    }

    public void OnUncoverd()
    {

    }

    public void AddFlag()
    {
        elementState = ElementState.Marked;
    }

    public void RemoveFlag()
    {
        elementState = ElementState.Covered;
    }
}

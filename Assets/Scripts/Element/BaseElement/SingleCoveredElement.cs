using UnityEngine;
using DG.Tweening;

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

    public override void OnPlayerStand()
    {
        switch (elementState)
        {
            case ElementState.Covered:
                UnCoveredElement();
                break;
            case ElementState.Uncovered:
                break;
            case ElementState.Marked:
                RemoveFlag();
                break;
        }
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

    public virtual void UnCoveredElement()
    {
        if(elementState == ElementState.Uncovered)
        {
            return;
        }
        UnCoveredElementSingle();
        OnUncoverd();
    }

    public virtual void UnCoveredElementSingle()
    {

    }

    public virtual void OnUncoverd()
    {

    }

    public void AddFlag()
    {
        elementState = ElementState.Marked;
        GameObject flag = Instantiate(GameManager.Instance.flagElement, transform);
        flag.name = "flagElement";
        flag.transform.DOLocalMoveY(0, 0.1f);
        Instantiate(GameManager.Instance.smokeEffect, transform);
    }

    public void RemoveFlag()
    {
        Transform flag = transform.Find("flagElement");
        if(flag != null)
        {
            elementState = ElementState.Covered;
            flag.DOLocalMoveY(0.15f, 0.1f).onComplete += () =>
            {
                Destroy(flag.gameObject);
            };
        }
    }
}

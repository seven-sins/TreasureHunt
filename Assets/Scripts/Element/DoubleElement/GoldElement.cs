using UnityEngine;

public class GoldElement : DoubleCoveredElement {

    public GoldType goldType;

    protected override void Awake()
    {
        base.Awake();
        elementContent = ElementContent.Gold;
    }

    public override void OnUncoverd()
    {
        Transform goldEffect = transform.Find("GoldEffect");
        if(goldEffect != null)
        {
            Destroy(goldEffect.gameObject);
        }
        // TODO 获得金币
        Debug.Log("Get a Gold");

        base.OnUncoverd();
    }

    public override void ConfirmSprite()
    {
        Transform goldEffect = transform.Find("GoldEffect");
        if (goldEffect != null)
        {
            Instantiate(GameManager.Instance.GoldEffect, transform).name = "GoldEffect";
        }
        LoadSprite(GameManager.Instance.goldSprites[(int)goldType]);
    }
}

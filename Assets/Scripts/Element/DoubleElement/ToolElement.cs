
using UnityEngine;

public class ToolElement : DoubleCoveredElement {

    public ToolType toolType;

    protected override void Awake()
    {
        base.Awake();
        elementContent = ElementContent.Tool;
    }

    public override void OnUncoverd()
    {
        // TODO 获得道具
        Debug.Log("Get a Tool");
        base.OnUncoverd();
    }

    public override void ConfirmSprite()
    {
        LoadSprite(GameManager.Instance.toolSprites[(int)toolType]);
    }
}

using UnityEngine;

public class ExitElement : CantCoveredElement
{

    protected override void Awake()
    {
        base.Awake();
        elementContent = ElementContent.Exit;
        ClearShadow();
        name = "Exit";
        LoadSprite(GameManager.Instance.exitSprites[Random.Range(0, GameManager.Instance.exitSprites.Length)]);
    }
}

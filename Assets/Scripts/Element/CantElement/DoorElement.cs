using UnityEngine;

public class DoorElement : CantCoveredElement
{

    protected override void Awake()
    {
        base.Awake();
        elementContent = ElementContent.Door;
        LoadSprite(GameManager.Instance.doorSprites[Random.Range(0, GameManager.Instance.doorSprites.Length)]);
    }
}

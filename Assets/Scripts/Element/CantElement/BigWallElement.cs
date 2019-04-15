using UnityEngine;

public class BigWallElement : CantCoveredElement {

    protected override void Awake()
    {
        base.Awake();
        elementContent = ElementContent.BigWall;
        // 随机加载一张大墙的图片
        LoadSprite(GameManager.Instance.bigwallSprites[Random.Range(0, GameManager.Instance.bigwallSprites.Length)]);
    }
}

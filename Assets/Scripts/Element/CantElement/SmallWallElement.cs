using UnityEngine;

public class SmallWallElement : CantCoveredElement {

    protected override void Awake()
    {
        base.Awake();
        elementContent = ElementContent.SmallWall;
        // 随机加载一张大墙的图片
        ClearShadow(); // 小墙没有阴影， 移除阴影
        LoadSprite(GameManager.Instance.smallwallSprites[Random.Range(0, GameManager.Instance.smallwallSprites.Length)]);
    }
}

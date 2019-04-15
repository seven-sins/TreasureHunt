using UnityEngine;

public class EnemyElement : CantCoveredElement {

    protected override void Awake()
    {
        base.Awake();
        elementContent = ElementContent.Enemy;
        ClearShadow();
        LoadSprite(GameManager.Instance.enemySprites[Random.Range(0, GameManager.Instance.enemySprites.Length)]);
    }
}

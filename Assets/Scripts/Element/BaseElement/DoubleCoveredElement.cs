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
}

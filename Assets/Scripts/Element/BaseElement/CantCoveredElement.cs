using UnityEngine;

public class CantCoveredElement : BaseElement {

    protected override void Awake()
    {
        base.Awake();
        elementType = ElementType.CantCovered;
        elementState = ElementState.Uncovered;
    }
}

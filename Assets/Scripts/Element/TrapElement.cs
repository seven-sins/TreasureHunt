using UnityEngine;

public class TrapElement : SingleCoveredElement {

    protected override void Awake()
    {
        base.Awake();
        elementState = ElementState.Covered;
        elementContent = ElementContent.Trap;
    }

    public override void UnCoveredElementSingle()
    {

    }

    public override void OnUncoverd()
    {

    }
}

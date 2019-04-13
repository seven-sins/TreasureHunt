using UnityEngine;

public class NumberElement : SingleCoveredElement
{

	protected override void Awake()
    {
        base.Awake();
        elementState = ElementState.Covered;
        elementContent = ElementContent.Number;
    }

    public override void OnMiddleMouseButton()
    {
        
    }

    public override void UnCoveredElementSingle()
    {

    }

    public override void OnUncoverd()
    {
        
    }
}

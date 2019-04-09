using UnityEngine;

public class BaseElement : MonoBehaviour {

    public int x;
    public int y;
    public ElementState elementState;
    public ElementType elementType;
    public ElementContent elementContent;

    protected virtual void Awake()
    {
        x = (int)transform.position.x;
        y = (int)transform.position.y;
        name = "(" + x+ "," + y + ")";
    }

    public virtual void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(2) && elementState == ElementState.Uncovered)
        {
            OnMiddleMouseButton();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnLeftMouseButton();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            OnRightMouseButton();
        }
    }

    public virtual void OnPlayerStand()
    {

    }

    public virtual void OnLeftMouseButton()
    {
        OnPlayerStand();
    }

    public virtual void OnMiddleMouseButton()
    {

    }

    public virtual void OnRightMouseButton()
    {

    }

    /// <summary>
    /// 切换当前无素的图片
    /// </summary>
    /// <param name="sprite">要切换的图片</param>
    protected void LoadSprite(Sprite sprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}

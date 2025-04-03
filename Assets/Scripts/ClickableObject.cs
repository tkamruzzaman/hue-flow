using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        print(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print(eventData);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print(eventData);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print(eventData);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        print(eventData);

    }

    void OnMouseDown()
    {
        print("Down");
    }

    void OnMouseUp()
    {
        print("Up");
    }

}

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemButton : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    public UnityEvent onLeft;
    public UnityEvent onRight;

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            onLeft.Invoke();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            onRight.Invoke();
        }
    }

   
}

using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

    public ItemTemplate itemTemplate;

    [HideInInspector]
    public bool isMouseOnItem = false;

    private void OnMouseEnter()
    {
        isMouseOnItem = true;
    }

    private void OnMouseExit()
    {
        isMouseOnItem = false;
    }

}

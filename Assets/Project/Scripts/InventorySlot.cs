using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    ItemTemplate item;
    public Image itemIcon;

    public void AddItemToSlot(ItemTemplate newItem)
    {
        item = newItem;

        itemIcon.sprite = item.icon;
        itemIcon.enabled = true;
    }

    public void ClearItemFromSlot()
    {
        item = null;

        itemIcon.sprite = null;
        itemIcon.enabled = false;
    }

    
}

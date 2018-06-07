using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour {

    #region Singleton

    public static InventorySystem instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
            return;
        }

        instance = this;
    }

    #endregion

    #region Delegate
    public delegate void OnItemStatusChanged();
    public OnItemStatusChanged onItemStatusChangedCallback;
    #endregion

    public List<ItemTemplate> items = new List<ItemTemplate>();
    int inventoryCapacity = 40;

    public bool AddItem(ItemTemplate item)
    {
        if (items.Count >= inventoryCapacity)
        {
            ItemManager.instance.dialogCloud.text = "Not enough space";
            return false;
        }

        items.Add(item);

        if (onItemStatusChangedCallback != null)
        {
            onItemStatusChangedCallback.Invoke();
        }

        return true;
    }

    public void RemoveItem(ItemTemplate item)
    {
        items.Remove(item);

        if (onItemStatusChangedCallback != null)
        {
            onItemStatusChangedCallback.Invoke();
        }
    }
}

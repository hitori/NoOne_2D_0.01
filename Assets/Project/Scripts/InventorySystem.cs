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

    public List<ItemTemplate> items = new List<ItemTemplate>(); // Создает лист айтемов, куда добавляеются новые айтемы при клике на них (не путать с ГУИ)
    int inventoryCapacity = 40; // Размер листа айтемов, сколько вещей может взять игрок (не путать с количеством ячеек в окне инвентаря)

    // Добавляет предмет в список
    public bool AddItem(ItemTemplate item)
    {
        if (items.Count >= inventoryCapacity) // проверяет, не превышает ли количество предметов из листа айтемов вместимость инвентаря
        {
            ItemManager.instance.dialogCloud.text = "Not enough space";
            return false;
        }

        items.Add(item);

        // Сообщает всем подписанным функциям об изменении статуса айтема
        if (onItemStatusChangedCallback != null)
        {
            onItemStatusChangedCallback.Invoke();
        }

        return true;
    }

    // Удаляет айтем из листа айтемов
    public void RemoveItem(ItemTemplate item)
    {
        items.Remove(item);

        if (onItemStatusChangedCallback != null)
        {
            onItemStatusChangedCallback.Invoke();
        }
    }
}

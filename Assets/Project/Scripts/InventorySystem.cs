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
    public ItemTemplate[] hands = new ItemTemplate[2];
    [HideInInspector]
    public int currentWeaponIndex;

    private int inventoryCapacity = 40; // Размер листа айтемов, сколько вещей может взять игрок (не путать с количеством ячеек в окне инвентаря)
    private ItemTemplate previousWeaponEquipped;




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

    public void EquipWeapon(ItemTemplate item)
    {
        if (item.currentWeaponClass != ItemTemplate.weaponClass.notAWeapon)
        {

            UnequipWeapon();
            
            if (item.isItTwoHandedWeapon)
            {
                for (int i = 0; i < hands.Length; i++)
                {
                    hands[i] = item;
                    Debug.Log(i);
                }
            }
            else hands[0] = item;

            currentWeaponIndex = (int)item.currentWeaponClass;
            previousWeaponEquipped = item;
        }
    }

    public void UnequipWeapon()
    {
        if (previousWeaponEquipped != null)
        {
            AddItem(previousWeaponEquipped);
        }

        for (int i = 0; i < hands.Length; i++)
        {
            hands[i] = null;
        }

        currentWeaponIndex = 0;
    }
}

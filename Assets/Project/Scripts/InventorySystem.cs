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

    #region Delegate OnItemStatusChanged()
    public delegate void OnItemStatusChanged();
    public OnItemStatusChanged onItemStatusChangedCallback;
    #endregion

    #region Delegate OnItemEquipped() // Пока нигде не используется
    public delegate void OnItemEquipped();
    public OnItemStatusChanged onItemEquippedCallback;
    #endregion 

    public List<ItemTemplate> items = new List<ItemTemplate>(); // Создает лист айтемов, куда добавляеются новые айтемы при клике на них (не путать с ГУИ)
    public ItemTemplate[] hands = new ItemTemplate[2];
    [HideInInspector]
    public int currentWeaponIndex;
    public Transform weaponHolder;
    public ItemTemplate equippedWeapon;

    private int inventoryCapacity = 40; // Размер листа айтемов, сколько вещей может взять игрок (не путать с количеством ячеек в окне инвентаря)
    private ItemTemplate previousWeaponEquipped;
    private GameObject instantiatedWeapon;
    


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

    public ItemTemplate EquipWeapon(ItemTemplate weapon)
    {
        if (weapon.currentWeaponClass != ItemTemplate.weaponClass.notAWeapon)
        {

            UnequipWeapon();

            if (weapon.isItTwoHandedWeapon)
            {
                for (int i = 0; i < hands.Length; i++)
                {
                    hands[i] = weapon;
                }
            }
            else hands[0] = weapon;


            instantiatedWeapon = Instantiate(weapon.itemModel, weaponHolder);
            instantiatedWeapon.transform.localPosition = Vector3.zero;
            instantiatedWeapon.transform.localEulerAngles = Vector3.zero;


            instantiatedWeapon.GetComponent<Collider>().enabled = false;
            currentWeaponIndex = (int)weapon.currentWeaponClass;
            previousWeaponEquipped = weapon;

            equippedWeapon = weapon;
        }

        return equippedWeapon;
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

        Destroy(instantiatedWeapon);
        currentWeaponIndex = 0;
    }
}

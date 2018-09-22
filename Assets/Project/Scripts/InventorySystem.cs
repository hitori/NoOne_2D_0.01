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

    public List<ItemTemplate> items = new List<ItemTemplate>(); // Создает лист айтемов, куда добавляеются новые айтемы при клике на них (не путать с ГУИ)
    public ItemTemplate[] hands = new ItemTemplate[2];
    [HideInInspector]
    public int currentWeaponIndex;
    public Transform weaponHolder;
    public Item_Equipment_Weapon equippedWeapon;

    private int inventoryCapacity = 40; // Размер листа айтемов, сколько вещей может взять игрок (не путать с количеством ячеек в окне инвентаря)
    private ItemTemplate previousWeaponEquipped;

    [HideInInspector]
    public GameObject instantiatedWeapon;
    //public ParticleSystem muzzleFlashParticles;

    static int pistolAmmoInInv;
    static int rifleAmmoInInv;
    static int shotgunAmmoInInv;
    static int sniperAmmoInInv;


    private bool isWeaponEquipped = false;


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

    //public Item_Equipment_Weapon EquipWeapon(Item_Equipment_Weapon weapon)
    //{
    //    UnequipWeapon();

    //    if (weapon.currentWeaponClass == Item_Equipment_Weapon.weaponClass.meleeLong || weapon.currentWeaponClass == Item_Equipment_Weapon.weaponClass.firearmsAssaultRifle) // оружие является двуручным
    //    {
    //        for (int i = 0; i < hands.Length; i++)
    //        {
    //            hands[i] = weapon;
    //        }
    //    }
    //    else hands[0] = weapon;


    //    instantiatedWeapon = Instantiate(weapon.itemPrefab, weaponHolder);
    //    instantiatedWeapon.transform.localPosition = Vector3.zero;
    //    instantiatedWeapon.transform.localEulerAngles = Vector3.zero;


    //    instantiatedWeapon.GetComponent<Collider>().enabled = false;
    //    currentWeaponIndex = (int)weapon.currentWeaponClass;
    //    previousWeaponEquipped = weapon;

    //    equippedWeapon = weapon;

    //    //if(onItemEquippedCallback != null)
    //    //{
    //    //    onItemEquippedCallback.Invoke(instantiatedWeapon);
    //    //}


    //    Instantiate(weapon.muzzleFlashObject, instantiatedWeapon.transform.GetChild(instantiatedWeapon.transform.childCount - 1).transform); // инстанциирует muzzleFlash в точке, которая является последним ребенком в firepoint


    //    return equippedWeapon; // нужен ли ретурн???
        
    //}

    //public void UnequipWeapon()
    //{
    //    if (previousWeaponEquipped != null)
    //    {
    //        AddItem(previousWeaponEquipped);
    //    }

    //    for (int i = 0; i < hands.Length; i++)
    //    {
    //        hands[i] = null;
    //    }

    //    Destroy(instantiatedWeapon);
    //    currentWeaponIndex = 0;
    //}

   

}

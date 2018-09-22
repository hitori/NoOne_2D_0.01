using UnityEngine;


public enum EquipmentSlot { feet, legs, legsProtection, belt, body, bodyProtection, head, weapon } // класс обмундирования: ступни, ноги, защита ног (наколенники, щитки), пояс, тело, защита тела (жилеты, куртки), голова
// ОбЪявляем тут, чтобы был доступ из других скриптов



[CreateAssetMenu(fileName = "Equipment Item", menuName = "Items/Equipment/Create an equipment item")]
public class Equipment : ItemTemplate {

    #region Delegate OnItemEquipped()
    public delegate void OnItemEquipped();
    public OnItemEquipped OnItemEquippedCallback;
    #endregion

    public EquipmentSlot equipmentSlot; // класс обмундирования: ступни, ноги, защита ног (наколенники, щитки), пояс, тело, защита тела (жилеты, куртки), голова

    //public bool isEquipmentForFeet;
    //public bool isEquipmentForLegs;
    //public bool isEquipmentForLegProt;
    //public bool isEquipmentForBelt;
    //public bool isEquipmentForBody;
    //public bool isEquipmentForBodyProt;
    //public bool isEquipmentForHead;
    //public bool isEquipmentAWeapon;


    //public List<ItemTemplate> items = new List<ItemTemplate>(); // Создает лист айтемов, куда добавляеются новые айтемы при клике на них (не путать с ГУИ)
    //public ItemTemplate[] hands = new ItemTemplate[2];
    //[HideInInspector]
    //public int currentWeaponIndex;
    //public Transform weaponHolder;
    //public Item_Equipment_Weapon equippedWeapon;

    //private int inventoryCapacity = 40; // Размер листа айтемов, сколько вещей может взять игрок (не путать с количеством ячеек в окне инвентаря)
    //private ItemTemplate previousWeaponEquipped;

    //[HideInInspector]
    //public GameObject instantiatedWeapon;
    //public ParticleSystem muzzleFlashParticles;



    //static int pistolAmmoInInv;
    //static int rifleAmmoInInv;
    //static int shotgunAmmoInInv;
    //static int sniperAmmoInInv;


    //private bool isWeaponEquipped = false;



    public override void Use()
    {
        base.Use();
        Debug.Log("Using item from Item_Equipment");
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory(); // метод из ItemTemplate поэтому доступ есть напрямую
    }

    


}

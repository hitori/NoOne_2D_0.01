using UnityEngine;

//[CreateAssetMenu (fileName = "Pickable Item", menuName = "Items/Create pickable item")]
public class ItemTemplate : ScriptableObject {

    public new string name; // название предмета
    public string description; // описание предмета. Появляется, когда наводишь на предмет мышку
    public Sprite icon = null; // иконка предмета в инвентаре
    //public bool isItTwoHandedWeapon = false;
    public GameObject itemPrefab;
    //public float firerate;
    //public int damage;
    //public int maxAmmo;
    //public int ammoClipCapacity;
    //public float reloadTime;
    //public AudioClip attackSound;
    //public AudioClip reloadSound;
    //public GameObject muzzleFlashObject;
    //public AudioClip noAmmoSound;
    //public Transform gunBurrel;
    //public Vector3 rHandPos; // позиция правой руки (вносится вручную после запуска игры)
    //public Vector3 rHanRot; // поворот правой руки (вносится вручную после запуска игры)
    //public Transform lHand_target; // куда должен стремиться IK левой руки

    //#region классы предметов
    public enum itemClass { consumable, equipment, ammo, keys, trading }; // класс предмета: продукты, обмундирование, патроны, ключи, предмет для обмена у торговца
    public itemClass currentClass;
    //public bool isItemConsumable;
    //public bool isItemAnEquipment;
    //public bool isItemAnAmmo;
    //public bool isItemAKey;
    //public bool isItemForATrade;

    //public enum consumableClass { food, drinks, energyBars } // класс продуктов: еда, питье, энергетические батончики
    //public consumableClass currentConsumableClass;
    ////public bool isConsumableIsFood;
    ////public bool isConsumableIsDrinks;
    ////public bool isConsumableIsEnergyBar;

    //public enum equipmentClass { feet, legs, legsProtection, belt, body, bodyProtection, head, weapon } // класс обмундирования: ступни, ноги, защита ног (наколенники, щитки), пояс, тело, защита тела (жилеты, куртки), голова
    //public equipmentClass currentEquipmentClass;
    ////public bool isEquipmentForFeet;
    ////public bool isEquipmentForLegs;
    ////public bool isEquipmentForLegProt;
    ////public bool isEquipmentForBelt;
    ////public bool isEquipmentForBody;
    ////public bool isEquipmentForBodyProt;
    ////public bool isEquipmentForHead;
    ////public bool isEquipmentAWeapon;

    //public enum ammoClass { handgun, assaultRifle, sniperRifle, shotgun } // класс патронов: для пистолета, автомата, снайперской винтовки, дробовика
    //public ammoClass currentAmmoClass;
    ////public bool isAmmoForHandgun;
    ////public bool isAmmoForAssaultRifle;
    ////public bool isAmmoForSniperRifle;
    ////public bool isAmmoForShotgun;

    //public enum keysClass { key, cardKey, tabletKey, smallKey, lockpick } // класс ключей: обычный ключ (двери), ключ-карта, ключ-таблетка(домофон), маленький ключ (ящик, сундук), отмычка (заменяею любой кроме ключ-карты и таблетки)
    //public keysClass currentKeyClass;
    ////public bool isKeyAKey;
    ////public bool isKeyACard;
    ////public bool isKeyATablet;
    ////public bool isKeyASmallKey;
    ////public bool isKeyALockpick;

    //public enum weaponClass { meleeShort, meleeLong, firearmsPistol, firearmsAssaultRifle }; // класс оружия: одноручное холодное, двуручное холодное, одноручное огнестрельное, двуручное огнестрельное 
    //public weaponClass currentWeaponClass;
    ////public bool isWeaponMeleeShort;
    ////public bool isWeaponMeleeLong;
    ////public bool isWeaponFirearmsPistol;
    ////public bool isWeaponFirearmsRifle;
    //#endregion

    
    public virtual void Use()
    {
        Debug.Log("Using item from Item Template");
    }


    public virtual void LeftButtonClickMain()
    {

        if (currentClass == itemClass.consumable)
        {
            Debug.Log("Eating " + name);
            RemoveFromInventory();
        }

        //else if (currentClass == itemClass.equipment)
        //{
        //    Debug.Log("Equipped " + name);
        //    InventorySystem.instance.EquipWeapon(this);
            
            
        //    // доработать с учетом замены текущей одежды

        //    /*
        //    switch (currentWeaponClass)
        //    {
        //        case 0:
        //            Debug.Log("Can't equip, not a weapon");
        //            break;

        //        case 1:

        //    }
        //    */


        //    InventorySystem.instance.RemoveItem(this);
        //}

        else if (currentClass == itemClass.ammo)
        {
            Debug.Log("Reloaded with " + name);
            // доработать с учетом количества патронов
        }

        else if (currentClass == itemClass.keys)
        {
            Debug.Log("Used key " + name);
            // доработать с учетом проверки правильности ключа
            RemoveFromInventory();
        }

        else if (currentClass == itemClass.trading)
        {
            Debug.Log("I can trade this " + name + "for something useful");
        }

        
    }

    //public virtual void RightButtonClickMain()
    //{
    //    Debug.Log("Right click use of " + name);
    //}

    public virtual void BreakItemMain()
    {
        Debug.Log("What's inside " + name + " I wonder");
        RemoveFromInventory();
    }

    public virtual void ViewItemMain()
    {
        Debug.Log("Let's see.." + name);
    }

    public virtual void DropItemMain()
    {
        Debug.Log("Dropped " + name);
        RemoveFromInventory();
    }

    public virtual void RemoveFromInventory()
    {
        InventorySystem.instance.RemoveItem(this);
    }
}

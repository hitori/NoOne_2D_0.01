using UnityEngine;

[CreateAssetMenu (fileName = "Pickable Item", menuName = "Items/Create pickable item")]
public class ItemTemplate : ScriptableObject {

    public new string name;
    public string description;
    public Sprite icon = null;
    public enum itemClass {consumable, equipment, ammo, keys, trading};
    public itemClass currentClass;
    public bool crafting = false;
    public enum weaponClass { notAWeapon, meleeShort, meleeLong, firearmsPistol, firearmsAssaultRifle};
    public weaponClass currentWeaponClass;
    public bool isItTwoHandedWeapon = false;
    public GameObject itemModel;
    public float firerate;
    public int damage;
    public int maxAmmo;
    public int ammoClipCapacity;
    public float reloadTime;
    public AudioClip attackSound;
    public AudioClip reloadSound;


    public virtual void LeftButtonClickMain()
    {

        if (currentClass == itemClass.consumable)
        {
            Debug.Log("Eating " + name);
            InventorySystem.instance.RemoveItem(this);
        }

        else if (currentClass == itemClass.equipment)
        {
            Debug.Log("Equipped " + name);
            InventorySystem.instance.EquipWeapon(this);
            
            
            // доработать с учетом замены текущей одежды

            /*
            switch (currentWeaponClass)
            {
                case 0:
                    Debug.Log("Can't equip, not a weapon");
                    break;

                case 1:

            }
            */


            InventorySystem.instance.RemoveItem(this);
        }

        else if (currentClass == itemClass.ammo)
        {
            Debug.Log("Reloaded with " + name);
            // доработать с учетом количества патронов
        }

        else if (currentClass == itemClass.keys)
        {
            Debug.Log("Used key " + name);
            // доработать с учетом проверки правильности ключа
            InventorySystem.instance.RemoveItem(this);
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
        InventorySystem.instance.RemoveItem(this);
    }

    public virtual void ViewItemMain()
    {
        Debug.Log("Let's see.." + name);
    }

    public virtual void DropItemMain()
    {
        Debug.Log("Dropped " + name);
        InventorySystem.instance.RemoveItem(this);
    }


}

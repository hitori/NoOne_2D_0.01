using UnityEngine;

public enum weaponClass { meleeShort, meleeLong, firearmsPistol, firearmsAssaultRifle }; // класс оружия: одноручное холодное, двуручное холодное, одноручное огнестрельное, двуручное огнестрельное 

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Equipment/Weapon")]
public class Item_Equipment_Weapon : Equipment
{
    //для всех видов оружия
    public weaponClass currentWeaponClass;
    public bool isTwoHanded;
    public int damage;
    public AudioClip attackSound;
    public Vector3 rHandPos; // позиция правой руки (вносится вручную после запуска игры)
    public Vector3 rHanRot; // поворот правой руки (вносится вручную после запуска игры)
    public Transform lHand_target; // куда должен стремиться IK левой руки


    //Для огнестрела
    public float fireRate;
    public int maxAmmo;
    public int ammoClipCapacity;
    public float reloadTime;
    public AudioClip reloadSound;
    public GameObject muzzleFlashObject;
    public AudioClip noAmmoSound;
    public Transform gunBurrel;


    public override void Use()
    {
        base.Use();
        Debug.Log("Using item from Item_Equipment_Weapon");
        WeaponManager.instance.EquipWeapon(this);
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

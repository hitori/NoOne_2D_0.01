using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    #region Singleton

    public static WeaponManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of EquipmentManager found");
            return;
        }

        instance = this;
    }

    #endregion

    #region Delegate OnWeaponEquipped()
    public delegate void OnWeaponEquipped(Item_Equipment_Weapon equippedWeapon, GameObject instantiatedWeapon);
    public OnWeaponEquipped onWeaponEquippedCallback;
    #endregion 

    [HideInInspector]
    public Item_Equipment_Weapon[] hands = new Item_Equipment_Weapon[2];
    [HideInInspector]
    public GameObject instantiatedWeapon;
    public Transform weaponHolder;
    public int currentWeaponIndex;
    [HideInInspector]
    public Item_Equipment_Weapon equippedWeapon;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipWeapon();
        }
    }


    public void EquipWeapon(Item_Equipment_Weapon weapon)
    {
        if (weapon.isTwoHanded)
        {
            for (int i = 0; i < hands.Length; i++)
            {
                hands[i] = weapon;
            }
        }

        else
        {
            hands[0] = weapon;
            hands[1] = null;
        }
        

        equippedWeapon = weapon;

        instantiatedWeapon = Instantiate(weapon.itemPrefab, weaponHolder);
        instantiatedWeapon.transform.localPosition = Vector3.zero;
        instantiatedWeapon.transform.localEulerAngles = Vector3.zero;
        instantiatedWeapon.GetComponent<Collider>().enabled = false;

        currentWeaponIndex = (int)weapon.currentWeaponClass;// для проигрывания правильной анимации "держать оружие"
        //previousWeaponEquipped = weapon;


        Instantiate(weapon.muzzleFlashObject, instantiatedWeapon.transform.GetChild(instantiatedWeapon.transform.childCount - 1).transform); // инстанциирует muzzleFlash в точке, которая является последним ребенком в firepoint

        if (onWeaponEquippedCallback != null)
        {
            onWeaponEquippedCallback.Invoke(equippedWeapon, instantiatedWeapon);
        }
    }

    public void UnequipWeapon()
    {
        for (int i = 0; i < hands.Length; i++)
        {
            hands[i] = null;
        }

        Destroy(instantiatedWeapon); // заменить на pooling object
        currentWeaponIndex = 0;
    }
}

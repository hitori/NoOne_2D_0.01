using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareWeapon : MonoBehaviour {

    InventorySystem inventory;
    ItemTemplate instantiatedWeapon;

	void Start () {
        inventory = InventorySystem.instance;
        inventory.onItemEquippedCallback += InstantiateMuzzleFlash;
        
	}
	
	void InstantiateMuzzleFlash(GameObject weaponInHands)
    {
        instantiatedWeapon = weaponInHands.GetComponent<ItemTemplate>();
        Instantiate(instantiatedWeapon.muzzleFlashObject, weaponInHands.transform.GetChild(weaponInHands.transform.childCount - 1).transform); // инстанциирует muzzleFlash в точке, которая является последним ребенком в firepoint
    }
}

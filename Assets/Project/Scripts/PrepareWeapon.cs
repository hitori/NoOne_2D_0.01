using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareWeapon : MonoBehaviour {

    InventorySystem inventory;

	void Start () {
        inventory = InventorySystem.instance;
        inventory.onItemEquippedCallback += InstantiateMuzzleFlash;
	}
	
	void InstantiateMuzzleFlash()
    {
        //Instantiate(weapon.muzzleFlashObject, instantiatedWeapon.transform.GetChild(instantiatedWeapon.transform.childCount - 1).transform); // инстанциирует muzzleFlash в точке, которая является последним ребенком в firepoint
    }
}

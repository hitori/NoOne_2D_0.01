using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private bool isInAttackState = false;
    private int fistFightKnuckle;
    private Animator animator;
    private InventorySystem inventory;
    private bool attack = false;
    private float nextFire;
    private int currentAmmoInClip;
    private int currentAmmoInInventory;
    private bool isReloading;
    private AudioSource audioSource;

    void Start () {
        animator = GetComponent<Animator>();
        inventory = InventorySystem.instance;
        audioSource = GetComponent<AudioSource>();
        currentAmmoInClip = 30;
	}
	
	
	void Update () {

        if (Input.GetMouseButton(1) && !isReloading)
        {
            isInAttackState = true;

            if (inventory.equippedWeapon.currentWeaponClass != ItemTemplate.weaponClass.meleeShort && inventory.equippedWeapon.currentWeaponClass != ItemTemplate.weaponClass.meleeLong) //изменить enum weaponType. Сделать дополнительнгый список, в котором будет только ручное и огнестрельное оружие
            {
                Shoot();
            }

            else
            {
                Attack();
            }
        }
        else isInAttackState = false;

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            if (inventory.equippedWeapon.currentWeaponClass != ItemTemplate.weaponClass.meleeShort && inventory.equippedWeapon.currentWeaponClass != ItemTemplate.weaponClass.meleeLong)
            {
                StartCoroutine("Reload");
            }   
        }


        animator.SetBool("isInAttackState", isInAttackState);
        animator.SetInteger("FistFight", fistFightKnuckle);
        animator.SetInteger("currentWeaponClass", inventory.currentWeaponIndex);
        animator.SetBool("attack", attack);
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fistFightKnuckle = Random.Range(1, 3);
            attack = true;
        }

        else
        {
            attack = false;
        }
    }


    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentAmmoInClip > 0)
            {
                attack = true;
                audioSource.clip = inventory.equippedWeapon.attackSound;
                audioSource.Play();
                currentAmmoInClip--;

            }

            else
            {
                currentAmmoInClip = 0;
            }

        }

        else if (Input.GetMouseButton(0) && (Time.time > nextFire))
        {
            if (currentAmmoInClip > 0)
            {
                attack = true;
                audioSource.clip = inventory.equippedWeapon.attackSound;
                audioSource.Play();
                nextFire = Time.time + 1/inventory.equippedWeapon.firerate;
                currentAmmoInClip--;
            }

            else
            {
                currentAmmoInClip = 0;
            }

        }
        else
        {
            attack = false;
        }
    }

    IEnumerator Reload()
    {
        if (currentAmmoInInventory > 0)
        {
            isReloading = true;

            if ((currentAmmoInInventory - (inventory.equippedWeapon.ammoClipCapacity - currentAmmoInClip)) >= 0)
            {
                currentAmmoInClip = inventory.equippedWeapon.ammoClipCapacity;
                currentAmmoInInventory = currentAmmoInInventory - (inventory.equippedWeapon.ammoClipCapacity - currentAmmoInClip);
            }

            else
            {
                currentAmmoInClip = currentAmmoInClip + currentAmmoInInventory;
            }

            yield return new WaitForSeconds(inventory.equippedWeapon.reloadTime);

            isReloading = false;
        }

        else
        {
            Debug.Log("No more ammo!");
        }
    }
}

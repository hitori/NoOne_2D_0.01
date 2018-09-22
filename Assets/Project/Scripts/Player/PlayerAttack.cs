using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private bool attack; // переменная для проигрывания анимации атаки
    private bool isReloading; // идет ли процесс перезарядки. Останавливает возможность выстрела во время перезарядки
    private Animator animator;
    private AudioSource audioSource; // для проигрывания звука удара (выстрела)
    private float nextFire; // время, которое должно пройти до следующе атаки (выстрела). Зависит от fireRate из ItemTemplate
    private int currentAmmoInClip; // сколько сейчас патронов в обойме
    private int currentAmmoInInventory; // сколько сейчас патронов в инвентаре
    private int fistFightKnuckle;
    private InventorySystem inventory;
    private ParticleSystem muzzle; // вспышка от выстрела
    private WeaponManager weaponManager;


    void Start()
    {
        animator = GetComponent<Animator>();
        inventory = InventorySystem.instance;
        audioSource = GetComponent<AudioSource>();
        weaponManager = WeaponManager.instance;
        currentAmmoInInventory = 100; // общее число патронов на все оружия. ИСПРАВИТЬ
    }


    void Update()
    {
        Debug.Log(GloabalVars.isInAttackState_g);

        if (Input.GetButtonDown("Fire2") && !isReloading) // Если игрок нажал правую клавишу мыши и при этом не идет процесс перезарядки (GetButtonDown взят для ДЕБАГА! Изменить на GetButton)
        {
            GloabalVars.isInAttackState_g = !GloabalVars.isInAttackState_g; // Игрок входит в режим прицеливания. Для ДЕБАГА замениь на !GloabalVars.isInAttackState_g
        }
        //else GloabalVars.isInAttackState_g = false; // выключен для возможности прицеливаться по нажатию кнопки, а не удержанию


       

        if (Input.GetButton("Fire1") && (Time.time > nextFire) && GloabalVars.isInAttackState_g && !isReloading)
        {
            attack = true;
            if (weaponManager.equippedWeapon != null)
            {
                if (weaponManager.equippedWeapon.currentWeaponClass != weaponClass.meleeShort && weaponManager.equippedWeapon.currentWeaponClass != weaponClass.meleeLong) //изменить enum weaponType. Сделать дополнительнгый список, в котором будет только ручное и огнестрельное оружие
                {
                    ShootAutoFire();
                }
            }

            else
            {
                Attack();
            }
        }

        //else if (Input.GetButtonDown("Fire1") && GloabalVars.isInAttackState_g) // если  игрок нажал кнопку атаки
        //{
        //    attack = true; // Начинается атака (эта переменная постоянно передается в аниматор, чтобы играла соответствующая анимация)

        //    if (weaponManager.equippedWeapon != null) // если в руках есть оружие, то атакуем оружием
        //    {
        //        if (weaponManager.equippedWeapon.currentWeaponClass != weaponClass.meleeShort && weaponManager.equippedWeapon.currentWeaponClass != weaponClass.meleeLong) //изменить enum weaponType. Сделать дополнительнгый список, в котором будет только ручное и огнестрельное оружие
        //        {
        //            {
        //                ShootOneShot();
        //            }
        //        }
        //    }

        //    else // если в руках нет оружия, то бьем кулаками
        //    {
        //        Attack();
        //    }


        //}

        else
        {
            attack = false;
        }

        


        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            if ((weaponManager.equippedWeapon.currentWeaponClass != weaponClass.meleeShort && weaponManager.equippedWeapon.currentWeaponClass != weaponClass.meleeLong) || weaponManager.equippedWeapon != null)
            {
                StartCoroutine("Reload");
            }
        }


        animator.SetBool("isInAttackState", GloabalVars.isInAttackState_g);
        animator.SetInteger("FistFight", fistFightKnuckle);
        animator.SetInteger("currentWeaponClass", weaponManager.currentWeaponIndex);
        animator.SetBool("attack", attack);
    }



    void Attack()
    {
        fistFightKnuckle = Random.Range(1, 3);
    }


    void ShootOneShot()
    {
        if (currentAmmoInClip > 0)
        {
            audioSource.clip = weaponManager.equippedWeapon.attackSound;
            audioSource.Play();
            muzzle = weaponManager.instantiatedWeapon.transform.GetChild(weaponManager.instantiatedWeapon.transform.childCount - 1).GetComponentInChildren<ParticleSystem>();
            muzzle.Play();
            currentAmmoInClip--;
        }

        else
        {
            currentAmmoInClip = 0;
            audioSource.PlayOneShot(weaponManager.equippedWeapon.noAmmoSound);
        }
    }

    void ShootAutoFire()
    {
        if (currentAmmoInClip > 0)
        {
            audioSource.clip = weaponManager.equippedWeapon.attackSound;
            audioSource.Play();
            weaponManager.instantiatedWeapon.transform.GetChild(weaponManager.instantiatedWeapon.transform.childCount - 1).GetComponentInChildren<ParticleSystem>().Play();
            nextFire = Time.time + 1 / weaponManager.equippedWeapon.fireRate;
            currentAmmoInClip--;
        }

        else
        {
            audioSource.clip = weaponManager.equippedWeapon.noAmmoSound;

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

        }
    }

    IEnumerator Reload()
    {
        if (currentAmmoInInventory > 0)
        {
            isReloading = true;

            if ((currentAmmoInInventory - (weaponManager.equippedWeapon.ammoClipCapacity - currentAmmoInClip)) >= 0)
            {
                currentAmmoInClip = weaponManager.equippedWeapon.ammoClipCapacity;
                currentAmmoInInventory = currentAmmoInInventory - (weaponManager.equippedWeapon.ammoClipCapacity - currentAmmoInClip);
            }

            else
            {
                currentAmmoInClip = currentAmmoInClip + currentAmmoInInventory;
            }

            audioSource.clip = weaponManager.equippedWeapon.reloadSound;
            audioSource.Play();
            yield return new WaitForSeconds(weaponManager.equippedWeapon.reloadTime);

            isReloading = false;
        }

        else
        {
            audioSource.clip = weaponManager.equippedWeapon.noAmmoSound;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            Debug.Log("No more ammo!");
        }
    }

}

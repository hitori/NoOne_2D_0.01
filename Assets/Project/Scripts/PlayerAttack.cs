using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    static public bool isInAttackState = false; // STATIC
    private int fistFightKnuckle;
    private Animator animator;
    private InventorySystem inventory;
    private bool attack = false;
    private float nextFire;
    private int currentAmmoInClip;
    private int currentAmmoInInventory;
    private bool isReloading;
    private AudioSource audioSource;

    ParticleSystem muzzle;
    PlayerMovement playermovementScript;

    Transform chest;
    public GameObject target;
    private IKHandler ikHandlerScript;
    private Vector3 lookPos;


    int maxAngleToAim = 30; // насколько вверх можно поднимать оружие (прицеливаться)
    int minAngleToAim = 90; // насколько вниз можно опускать оружие (прицеливаться)
    public Vector3 offset;
    Vector3 diff2;


    void Start () {
        animator = GetComponent<Animator>();
        inventory = InventorySystem.instance;
        audioSource = GetComponent<AudioSource>();
        currentAmmoInInventory = 100;

        //
        playermovementScript = GetComponent<PlayerMovement>();
        ikHandlerScript = GetComponent<IKHandler>();
        chest = animator.GetBoneTransform(HumanBodyBones.Chest);
        //
    }
	
	
	void Update () {

        lookPos = ikHandlerScript.targetPos;

        if (Input.GetMouseButton(1) && !isReloading)
        {
            isInAttackState = true;

            

            if (Input.GetMouseButtonDown(0))
            {
                attack = true;
                if (inventory.equippedWeapon != null)
                {
                    if (inventory.equippedWeapon.currentWeaponClass != ItemTemplate.weaponClass.meleeShort && inventory.equippedWeapon.currentWeaponClass != ItemTemplate.weaponClass.meleeLong) //изменить enum weaponType. Сделать дополнительнгый список, в котором будет только ручное и огнестрельное оружие
                    {
                        {
                            ShootOneShot();
                        }
                    }
                }

                else
                {
                    Attack();
                }


            }

            else if (Input.GetMouseButton(0) && (Time.time > nextFire))
            {
                attack = true;
                if (inventory.equippedWeapon != null)
                {
                    if (inventory.equippedWeapon.currentWeaponClass != ItemTemplate.weaponClass.meleeShort && inventory.equippedWeapon.currentWeaponClass != ItemTemplate.weaponClass.meleeLong) //изменить enum weaponType. Сделать дополнительнгый список, в котором будет только ручное и огнестрельное оружие
                    {
                        ShootAutoFire();
                    }
                }

                else
                {
                    Attack();
                }
            }

            else attack = false;
        }

        else isInAttackState = false;
 

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            if ((inventory.equippedWeapon.currentWeaponClass != ItemTemplate.weaponClass.meleeShort && inventory.equippedWeapon.currentWeaponClass != ItemTemplate.weaponClass.meleeLong) || inventory.equippedWeapon != null)
            {
                StartCoroutine("Reload");
            }   
        }


        animator.SetBool("isInAttackState", isInAttackState);
        animator.SetInteger("FistFight", fistFightKnuckle);
        animator.SetInteger("currentWeaponClass", inventory.currentWeaponIndex);
        animator.SetBool("attack", attack);
    }

    private void LateUpdate()
    {

        if (isInAttackState)
        {
            Vector3 diff = lookPos - this.transform.position;
            //diff.Normalize();
            //diff = transform.InverseTransformDirection(diff);
            float angle = Vector3.Angle(lookPos, this.transform.position);
            //float angle2 = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            //if (angle < maxAngleToAim)
            //    angle = maxAngleToAim;
            //if (angle > minAngleToAim)
            //    angle = minAngleToAim;
            //Debug.Log(angle);

            if (angle >= maxAngleToAim)// && angle <= minAngleToAim)
            {
                angle = maxAngleToAim;

                if (angle <= minAngleToAim)
                {
                    angle = minAngleToAim;
                    diff2 = diff;

                    //chest.transform.rotation = Quaternion.LookRotation(diff);
                    //chest.transform.rotation = Quaternion.Euler(angle, offset.y, offset.z);
                }


            }

            chest.LookAt(diff2);
        }


    }

    void Attack()
    {
        fistFightKnuckle = Random.Range(1, 3);
    }


    void ShootOneShot()
    {
        if (currentAmmoInClip > 0)
        {
            audioSource.clip = inventory.equippedWeapon.attackSound;
            audioSource.Play();
            muzzle = inventory.instantiatedWeapon.transform.GetChild(inventory.instantiatedWeapon.transform.childCount - 1).GetComponentInChildren<ParticleSystem>();
            //muzzle.transform.localPosition = Vector3.zero;
            //float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            //muzzle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            muzzle.Play();
            //inventory.weaponHolder.GetComponentInChildren<ParticleSystem>().Play();// проигрывает particle system, находящийся в ребенке (ищет во всех детях? Не слишком ли это нагружает систему?)
            currentAmmoInClip--;
        }

        else
        {
            currentAmmoInClip = 0;
            audioSource.PlayOneShot(inventory.equippedWeapon.noAmmoSound);
        }
    }

    void ShootAutoFire()
    {
        if (currentAmmoInClip > 0)
        {
            audioSource.clip = inventory.equippedWeapon.attackSound;
            audioSource.Play();
            inventory.instantiatedWeapon.transform.GetChild(inventory.instantiatedWeapon.transform.childCount - 1).GetComponentInChildren<ParticleSystem>().Play();
            nextFire = Time.time + 1 / inventory.equippedWeapon.firerate;
            currentAmmoInClip--;
        }

        else
        {
            currentAmmoInClip = 0;
            audioSource.clip = inventory.equippedWeapon.noAmmoSound;

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

            if ((currentAmmoInInventory - (inventory.equippedWeapon.ammoClipCapacity - currentAmmoInClip)) >= 0)
            {
                currentAmmoInClip = inventory.equippedWeapon.ammoClipCapacity;
                currentAmmoInInventory = currentAmmoInInventory - (inventory.equippedWeapon.ammoClipCapacity - currentAmmoInClip);
            }

            else
            {
                currentAmmoInClip = currentAmmoInClip + currentAmmoInInventory;
            }

            audioSource.clip = inventory.equippedWeapon.reloadSound;
            audioSource.Play();
            yield return new WaitForSeconds(inventory.equippedWeapon.reloadTime);

            isReloading = false;
        }

        else
        {
            audioSource.clip = inventory.equippedWeapon.noAmmoSound;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            Debug.Log("No more ammo!");
        }
    }

    
}

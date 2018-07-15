using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private bool isInAttackState = false;
    private int fistFightKnuckle;
    private Animator animator;
    private InventorySystem inventory;

    

    void Start () {
        animator = GetComponent<Animator>();
        inventory = InventorySystem.instance;
	}
	
	
	void Update () {

        if (Input.GetMouseButton(1))
        {
            isInAttackState = true;
        }
        else isInAttackState = false;


        //if (Input.GetMouseButton(1))
        //{
        //    isInAttackState = true;
        //    //if (Input.GetMouseButtonDown(0) && fistFightKnuckle != 1 && fistFightKnuckle != 2)
        //    //{
        //    //    fistFightKnuckle = Random.Range(1, 3);
        //    //}
        //    //else fistFightKnuckle = 0;


        //}
        //else isInAttackState = false;

        animator.SetBool("isInAttackState", isInAttackState);
        animator.SetInteger("FistFight", fistFightKnuckle);
        animator.SetInteger("currentWeaponClass", inventory.currentWeaponIndex);

    }
}

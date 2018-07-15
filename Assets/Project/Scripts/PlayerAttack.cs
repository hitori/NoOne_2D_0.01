using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private bool isInAttackState = false;
    private int fistFightKnuckle;
    private Animator animator;
    private InventorySystem inventory;
    private bool attack = false;
    

    void Start () {
        animator = GetComponent<Animator>();
        inventory = InventorySystem.instance;
	}
	
	
	void Update () {

        if (Input.GetMouseButton(1))
        {
            isInAttackState = true;

            if (Input.GetMouseButtonDown(0))
            {
                attack = true;
                fistFightKnuckle = Random.Range(1, 3);
            }
            else
            {
                attack = false;
                fistFightKnuckle = 0;
            }

            
        }
        else isInAttackState = false;


        animator.SetBool("isInAttackState", isInAttackState);
        animator.SetInteger("FistFight", fistFightKnuckle);
        animator.SetInteger("currentWeaponClass", inventory.currentWeaponIndex);
        animator.SetBool("attack", attack);
    }
}

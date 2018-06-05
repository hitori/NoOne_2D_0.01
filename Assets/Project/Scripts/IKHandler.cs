using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHandler : MonoBehaviour {

    Animator animator;
    Vector3 lookPos;
    Vector3 IK_lookPos;
    Vector3 targetPos;
    PlayerMovement playerMovementScript;

    public float lerpRate = 15f;
    public float updatelookPosThreshold = 2;
    public float lookWeight = .5f;
    public float bodyWeight = .1f;
    public float headWeight = .8f;
    public float clampWeight = 1;

    //float rightHandWeight = 1;
    //float leftHandWeight = 1;
    //public Transform rightHandTarget;
    //public Transform rightElbowTarget;
    //public Transform leftHandTarget;
    //public Transform leftElbowTarget;


    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMovementScript = GetComponent<PlayerMovement>();
    }

    private void OnAnimatorIK()
    {
        //animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
        //animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);

        //animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
        //animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);

        //animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandWeight);
        //animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandWeight);

        //animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
        //animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);

        //animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, rightHandWeight);
        //animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, leftHandWeight);

        //animator.SetIKHintPosition(AvatarIKHint.RightElbow, rightElbowTarget.position);
        //animator.SetIKHintPosition(AvatarIKHint.LeftElbow, leftElbowTarget.position);

        this.lookPos = playerMovementScript.lookPos;

        float distanceFromPlayer = Vector3.Distance(lookPos, transform.position);
        if (distanceFromPlayer > updatelookPosThreshold)
        {
            targetPos = lookPos;
        }

        //Debug.Log(targetPos + " " + lookPos + " " + IK_lookPos);

        IK_lookPos = Vector3.Lerp(IK_lookPos, targetPos, Time.deltaTime * lerpRate);

        animator.SetLookAtWeight(lookWeight, bodyWeight, headWeight, headWeight, clampWeight);
        animator.SetLookAtPosition(IK_lookPos);
    }
}

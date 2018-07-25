using UnityEngine;

public class IKHandler : MonoBehaviour {

    Animator animator;
    Vector3 lookPos;
    Vector3 IK_lookPos;
    Vector3 targetPos;
    PlayerMovement playerMovementScript;

    public float lerpRate = 15f;
    public float updatelookPosThreshold = 2; // величина, ограничивающая насколько Игрок нагибается (или поднимает голову вверх), чтобы увидеть курсор мыши
    public float lookWeight = .5f;
    public float bodyWeight = .1f;
    public float headWeight = .8f;
    public float clampWeight = .5f;

    //    float rightHandWeight = 1;
    //    float leftHandWeight = 1;
    //    public Transform rightHandTarget;
    //    public Transform rightElbowTarget;
    //    public Transform leftHandTarget;
    //    public Transform leftElbowTarget;

    private float angle;
    public Transform rightShoulder;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMovementScript = GetComponent<PlayerMovement>();
    }

    private void OnAnimatorIK()
    {
//        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
//        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);
//
//        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
//        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
//
        //animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandWeight);
        //animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandWeight);

        //animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
		//animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
//
//        animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, rightHandWeight);
//        animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, leftHandWeight);
//
//        animator.SetIKHintPosition(AvatarIKHint.RightElbow, rightElbowTarget.position);
//        animator.SetIKHintPosition(AvatarIKHint.LeftElbow, leftElbowTarget.position);

        this.lookPos = playerMovementScript.lookPos; // Присваивает значение lookPos значению из скрипта PlayerMovement

        float distanceFromPlayer = Vector3.Distance(lookPos, transform.position); //Рассчитывает расстояние от точки lookPos до позиции Игрока
        if (distanceFromPlayer > updatelookPosThreshold) // Если расстояние больше ограничения, то Игрок свободно смотрит на курсор мыши. Если курсор мыши слишком близко, то Игрок смотрит в предыдущую точку
        {
            targetPos = lookPos; // целевое положение приравнивается к положению мыши
        }

        IK_lookPos = Vector3.Lerp(IK_lookPos, targetPos, Time.deltaTime * lerpRate); // Взгляд плавно переходит от текущего положения на новое положение мыши

        animator.SetLookAtWeight(lookWeight, bodyWeight, headWeight, headWeight, clampWeight); // Присваиваем веса различным частям тела, участвующим при слежении за курсором мыши. Чем больше вес, тем сильнее часть тела вовлечена в анимацию просмотра
        animator.SetLookAtPosition(IK_lookPos); // Включаем функцию прсмотра за курсором мыши
    }

    void Update()
    {
        angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        rightShoulder.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}

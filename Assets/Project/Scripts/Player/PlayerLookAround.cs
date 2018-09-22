using UnityEngine;

public class PlayerLookAround : MonoBehaviour {

    [HideInInspector]
    public static Vector3 lookPos; // точка, куда смотрит игрок
    public float movementTurningSpeed = 10f; // скорость поворота на 180 градусов
    public float lerpRate = 15f; // уровень сглаживания
    public float updatelookPosThreshold = 2; // величина, ограничивающая минимальное расстояние до игрока, на который он смотрит за курсором (ближе него отслеживание курсора прекращается)
    public float lookWeight = 1f; //вес взгляда (1 = полностью смотрит на цель)
    public float bodyWeight = .4f; // вес для тела
    public float headWeight = .8f; // вес для головы

    private Transform lHand; // позиция левой руки. В скрипте приравнивается к позиции lHand_target из ItemTemplate (корректируется за счет lHand_target в префабе оружия)
    private Quaternion lHandRot; // вращение левой руки. В скрипте приравнивается к повороту lHand_target из ItemTemplate (корректируется за счет lHand_target в префабе оружия)
    private Transform rHand; // позиция правой руки. Настраивается вручную за счет right_hand в aim_pivot (правую руку нельзя также задать жесткой позицией, как и левую. Проверено. Они начинают конфликтовать с левой рукой, появляется дерганье рук)
    private Transform shoulder; // позиция кости плеча. Вводится, чтобы aimPivot вращался по оси плеча
    private Transform aimPivot; // фейковый объект, который содержит в себе два объекта, полностью копирующие координаты lHand и rHand. Вращая этот объект вокруг точки плеча получаем вращение оружия 
    private InventorySystem inventory;
    private Animator anim;
    private Vector3 targetPos; // куда поворачивать голову, следующая точка
    private Vector3 IK_lookPos; // куда поворачивать голову IK, текущая точка

    private Item_Equipment_Weapon weapon;
    private GameObject instantiatedWeapon;

    void Start()
    {
        anim = GetComponent<Animator>();
        shoulder = anim.GetBoneTransform(HumanBodyBones.RightShoulder).transform; // находим кость плеча
        inventory = InventorySystem.instance;

        // Создаем пустой объект aim_pivot
        aimPivot = new GameObject().transform;
        aimPivot.name = "aim_pivot";
        aimPivot.transform.parent = this.transform;

        // В aim_pivot создаем пустой объект lHand
        lHand = new GameObject().transform;
        lHand.name = "left_hand";
        lHand.transform.parent = aimPivot;

        // В aim_pivot создаем пустой объект rHand
        rHand = new GameObject().transform;
        rHand.name = "right_hand";
        rHand.transform.parent = aimPivot;

        WeaponManager.instance.onWeaponEquippedCallback += GetRHandPosition;
    }


    void Update()
    {
        GetMousePosition();
        RotateAroundY();

        if (WeaponManager.instance.hands[0] != null) // если в руках есть какое-лиюо оружие
        {
            GetLHandTransform();


        }
    }

    // Определяет текущее положение мышки. 
    // Координаты z точки попадания выравниваются с координатой z игрока (чтобы игрок смотрел строго прямо, а не влево).
    // Полученная точка является точкой, куда смотрит игрок (lookPos)
    void GetMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;
        lookPos = mousePosition;
    }

    void GetRHandPosition(Item_Equipment_Weapon weapon, GameObject instantiatedWeapon)
    {
        //выделить в отдельный метод и кэшировать
        rHand.localPosition = weapon.rHandPos; // задаем локальную позицию правой руки. Присваиваем ей значение из ItemTemplate
        Quaternion rotRight = Quaternion.Euler(weapon.rHanRot.x, weapon.rHanRot.y, weapon.rHanRot.z);
        rHand.localRotation = rotRight; //задаем локальное вращение правой руки. Присваиваем ей значение из ItemTemplate
    }


    // Поворот ТОЛЬКО вокруг оси у. Определяет направляение между текущим положением игрока и точкой, куда смотрит игрок.
    // y-составляющую вектора направления приравниваем к нулю, чтобы игрок смотрел строго прямо (иначе модель начнет крутиться по оси z)
    // Используем Slerp для плавного изменения вращения между текущим положением и тем, куда мы смотри
    void RotateAroundY()
    {
        Vector3 directionToLook = lookPos - transform.position;
        directionToLook.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(directionToLook);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * movementTurningSpeed);
    }

    // Присваиваем позицию и вращение lHand
    void GetLHandTransform()
    {
        lHandRot = WeaponManager.instance.instantiatedWeapon.transform.GetChild(1).transform.rotation; // поворот lHandRot присваивается к повороту lHand_target
        lHand.position = WeaponManager.instance.instantiatedWeapon.transform.GetChild(1).transform.position; // позиция lHand присваивается к позиции lHand_target
    }

    // Тут двигаем все IK, включаем слежение за курсором
    private void OnAnimatorIK()
    {
        float distanceFromPlayer = Vector3.Distance(lookPos, transform.position + Vector3.up * 0.8f);//Рассчитывает расстояние от точки lookPos до позиции Игрока (т.к. 'ноль' игрока находится в ногах, то поднимаеам на 0.8 метра)
        if (distanceFromPlayer > updatelookPosThreshold) // Если расстояние больше ограничения, то Игрок свободно смотрит на курсор мыши. Если курсор мыши слишком близко, то Игрок смотрит в предыдущую точку
        {
            targetPos = lookPos; // целевое положение приравнивается к текущему положению мыши
        }

        IK_lookPos = Vector3.Lerp(IK_lookPos, targetPos, Time.deltaTime * lerpRate); // Новая точка взгляда плавно переходит от предыдущего положения мыши на новое


        if (GloabalVars.isInAttackState_g) // если игрок в режиме прицеливания
        {
            aimPivot.position = shoulder.position; // Приравнивает положение фейкового aimPivot к положению плеча

            if (WeaponManager.instance.hands[0] != null) // есл в руках что-то есть
            {
                aimPivot.LookAt(lookPos); // Поворачивает aimPivot и за счет этого вращаются руки с оружием

                anim.SetLookAtWeight(lookWeight, bodyWeight, headWeight); // Присваиваем веса различным частям тела, участвующим при слежении за курсором мыши. Чем больше вес, тем сильнее часть тела вовлечена в анимацию просмотра
      
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f); // устанавливаем вес положения для левой руки
                anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f); // устанавливаем вес вращения для левой руки
                anim.SetIKPosition(AvatarIKGoal.LeftHand, lHand.position); // Включаем отслеживание положения IK для левой руки
                anim.SetIKRotation(AvatarIKGoal.LeftHand, lHandRot); // Включаем поворот IK для левой руки

                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f); // устанавливаем вес положения для правой руки
                anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f); // устанавливаем вес вращения для правой руки
                anim.SetIKPosition(AvatarIKGoal.RightHand, rHand.position); // Включаем отслеживание положения IK для правой руки
                anim.SetIKRotation(AvatarIKGoal.RightHand, rHand.rotation); // Включаем поворот IK для правой руки
            }
        }

        else
        {
            anim.SetLookAtWeight(lookWeight, .1f, headWeight); // Когда игрок в не в режиме прицеливания, то используем другой вес для тела, чтобы он поворачивал только головой
        }

        anim.SetLookAtPosition(IK_lookPos); // Игрок начинает следить за IK_lookPos
    }

    // Включаем отображение Gizmos
    private void OnDrawGizmos()
    {
        //луч из дула по направлению прямо

        if (WeaponManager.instance.hands[0] != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(WeaponManager.instance.instantiatedWeapon.transform.GetChild(0).transform.position, WeaponManager.instance.instantiatedWeapon.transform.GetChild(0).transform.forward * 10f);
        }

        //позиция мыши
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(lookPos, .1f);

        //Сфера, внутри которой lookPos не обновляется (чтобы игрок на ломал тело пытаясь посмотреть на свои колени) 
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 0.8f, updatelookPosThreshold);
    }
}

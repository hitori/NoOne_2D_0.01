using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour 
{

    float movementWalkSpeed = 2f;// скорость ходьбы
	float movementRunSpeed = 7f;// скорость бега
	float movementSneakSpeed = .8f; // скорость ходьбы когда крадешься
	float movementTurningSpeed = 10f; // скорость поворота на 180 градусов
	float movementAmount;
	bool isRunning = false;
	bool isSneaking = false;
    bool isFistFight = false;
    int fistFight;

	Vector2 velocity;
	Rigidbody rb;
	public Vector3 lookPos;//make it private somehow?
	Animator animator;



	//Transform modelTransform;
 //   public Transform fakeShoulderPosition;
 //   public Transform rightShoulderArmature;
 //   GameObject tempRightShoulderPosition;


	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator> ();
        //tempRightShoulderPosition = new GameObject();
        //tempRightShoulderPosition.name = transform.root.name + " rightshoulder IK helper";
	}
	

	void Update () 
	{
        if (EventSystem.current.IsPointerOverGameObject()) // блокирует клики мыши для перемещения игрока, когда включен инвентарь
        {
            return;
        }

		movementAmount = Input.GetAxis ("Horizontal") * movementWalkSpeed;

		if (Input.GetKey (KeyCode.LeftShift) && movementAmount != 0) { // если нажат шифт и игрок движется (перемещение != 0), то включается режим бега
			movementAmount = Input.GetAxis ("Horizontal") * movementRunSpeed;
			isRunning = true;
		} else
			isRunning = false; // когда игрок отжимает шифт или когда движение = 0, отключается режим бега

		if (Input.GetKey (KeyCode.LeftControl) && movementAmount != 0) { // если нажат контрол и игрок движется (перемещение != 0), то включается режим сникинга
			movementAmount = Input.GetAxis ("Horizontal") * movementSneakSpeed;
			isSneaking = true;
		} else
			isSneaking = false; // когда игрок отжимает контрол или когда движение = 0, отключается режим сникинга

        if (Input.GetMouseButton(1))
        {
            isFistFight = true;
            if (Input.GetMouseButtonDown(0) && fistFight != 1 && fistFight != 2)
            {
                fistFight = Random.Range(1, 3);
            }
            else fistFight = 0;
        }
        else isFistFight = false;
	}

	private void FixedUpdate()
	{
        if (EventSystem.current.IsPointerOverGameObject()) // блокирует клики мыши для перемещения игрока, когда включен инвентарь
        {
            return;
        }

        velocity = rb.velocity;

        if (movementAmount < 0)
        {
            velocity.x = movementAmount * .5f; // если игрок идет спиной, то сокращаем скорость движения назад в два раза (бег, шаг, сникинг)
        }
        else
        {
            velocity.x = movementAmount; // если игрок идет лицом, то сокращаем скорость движения не изменяется (бег, шаг, сникинг)
        }
        rb.velocity = velocity; // *time.fixeddeltatime???


        HandleAimingPos (); // Пускает луч из камеры в текущее положение мышки. На пересечении с задней стенкой сцены (коллайдером) регистрирует попадание (хит). 
							// Координаты z точки попадания выравниваются с координатой z игрока (чтобы игрок смотрел строго прямо, а не влево).
							// Полученная точка является точкой, куда смотрит игрок (lookPos)

		HandleRotation ();  // Поворот ТОЛЬКО вокруг оси у. Определяет направляение между текущим положением игрока и точкой, куда смотрит игрок.
							// y-составляющую вектора направления приравниваем к нулю, чтобы игрок смотрел строго прямо (иначе модель начнет крутиться по оси z)
							// Используем Slerp для плавного изменения вращения между текущим положением и тем, куда мы смотрим

		HandleAnimations (); // Передает значение перемещения в аниматор для изменения соответствующей анимации.
                             // если мышка находится левее игрока, то все анимации меняются местами (если этого не сделать, то движение вправо всегда будет с анимацией бега,
                             // даже если в этот момент игрок идет вправо спиной

	}
		
	void HandleAimingPos()
	// Пускает луч из камеры в текущее положение мышки. На пересечении с задней стенкой сцены (коллайдером) регистрирует попадание (хит). 
	// Координаты z точки попадания выравниваются с координатой z игрока (чтобы игрок смотрел строго прямо, а не влево).
	// Полученная точка является точкой, куда смотрит игрок (lookPos)
	{
        lookPos = Vector3.right;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			Vector3 lookP = hit.point;
            //Debug.Log(hit.collider.gameObject.name);
            //Debug.Log("Before " + lookP.z + " " + transform.position.z);
            lookP.z = transform.position.z;
            //Debug.Log("After " + lookP.z + " " + transform.position.z);
			lookPos = lookP;
		}

        //Debug.Log("hit point " + hit.point + ", lookPos " + lookPos);
	}

	void HandleRotation()
    // Поворот ТОЛЬКО вокруг оси у. Определяет направляение между текущим положением игрока и точкой, куда смотрит игрок.
    // y-составляющую вектора направления приравниваем к нулю, чтобы игрок смотрел строго прямо (иначе модель начнет крутиться по оси z)
    // Используем Slerp для плавного изменения вращения между текущим положением и тем, куда мы смотрим
    {
        Vector3 directionToLook = lookPos - transform.position;
		directionToLook.y = 0;
		Quaternion targetRotation = Quaternion.LookRotation (directionToLook);
		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.fixedDeltaTime * movementTurningSpeed);
	}

	void HandleAnimations()
	// Передает значение перемещения в аниматор для изменения соответствующей анимации.
	// если мышка находится левее игрока, то все анимации меняются местами (если этого не сделать, то движение вправо всегда будет с анимацией бега,
	// даже если в этот момент игрок идет вправо спиной
	{
		float animValue = movementAmount;

		if (lookPos.x < transform.position.x) {
			animValue = -animValue;
		}

		animator.SetFloat ("Movement", animValue, .1f, Time.deltaTime);
		animator.SetBool ("isRunning", isRunning);
		animator.SetBool ("isSneaking", isSneaking);
        animator.SetBool ("isFistFight", isFistFight);
        animator.SetInteger("FistFight", fistFight);
	}





    //void HandleShoulder()
    //{
    //    fakeShoulderPosition.LookAt(lookPos);

    //    Vector3 rightShoulderPos = rightShoulderArmature.TransformPoint(Vector3.zero);
    //    tempRightShoulderPosition.transform.position = rightShoulderPos;
    //    tempRightShoulderPosition.transform.parent = transform;

    //    fakeShoulderPosition.position = tempRightShoulderPosition.transform.position;
    //}

}

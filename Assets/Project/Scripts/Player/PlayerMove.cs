using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour {

    public float movementWalkSpeed = 2f;// скорость ходьбы
    public float movementRunSpeed = 7f;// скорость бега
    public float movementSneakSpeed = .8f; // скорость ходьбы когда крадешься

    private float movementAmount;
    private bool isRunning = false;
    private bool isSneaking = false;
    private Vector2 velocity;
    private Rigidbody rb;
    private Animator animator;
    private float animValue; // служит для поворота игрока вокруг оси у. Если курсор меняет сторону экрана (был справа от игрока, стал слева, например), то все все анимации проигрываются наоборот

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        movementAmount = Input.GetAxis("Horizontal") * movementWalkSpeed;

        if (Input.GetKey(KeyCode.LeftShift) && movementAmount != 0) // если нажат шифт и игрок движется (перемещение != 0), то включается режим бега
        { 
            movementAmount = Input.GetAxis("Horizontal") * movementRunSpeed;
            isRunning = true;
        }
        else
            isRunning = false; // когда игрок отжимает шифт или когда движение = 0, отключается режим бега

        if (Input.GetKey(KeyCode.LeftControl) && movementAmount != 0) // если нажат контрол и игрок движется (перемещение != 0), то включается режим сникинга
        { 
            movementAmount = Input.GetAxis("Horizontal") * movementSneakSpeed;
            isSneaking = true;
        }
        else
            isSneaking = false; // когда игрок отжимает контрол или когда движение = 0, отключается режим сникинга

        HandleAnimations();
    }

    private void FixedUpdate() // так как двигаем rigidbody, то лучше использовать Fixed Update
    {
        if (EventSystem.current.IsPointerOverGameObject()) // блокирует клики мыши для перемещения игрока, когда включен инвентарь
        {
            return;
        }

        velocity = rb.velocity; // сначала берем значение от rigidbody

        if (movementAmount < 0)
        {
            velocity.x = movementAmount * .5f; // если игрок идет спиной, то сокращаем скорость движения назад в два раза (бег, шаг, сникинг)
        }
        else
        {
            velocity.x = movementAmount; // если игрок идет лицом, то скорость движения не изменяется (бег, шаг, сникинг)
        }

        rb.velocity = velocity; // затем приравниваем значение rigidbody к измененному
    }


    // Передает значение перемещения в аниматор для изменения соответствующей анимации.
    // если мышка находится левее игрока, то все анимации меняются местами (если этого не сделать, то движение вправо всегда будет с анимацией бега,
    // даже если в этот момент игрок идет вправо спиной
    void HandleAnimations()
    {
        animValue = movementAmount;
        if (PlayerLookAround.lookPos.x < transform.position.x)
        {
            animValue = -animValue;
        }

        animator.SetFloat("Movement", animValue, .1f, Time.deltaTime); 
        animator.SetBool("isRunning", isRunning); // передаем в аниматор бежит ли игрок
        animator.SetBool("isSneaking", isSneaking); // передаем в аниматор крадется ли игрок
    }
}

using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform playerPosition; // позиция player'а
	public float smoothSpeed = 10f; // сглаживание движения камеры (чтобы она не очень резко искала playaer'а)
	public Vector3 offset; // сдвиг камеры, чтобы игрок был не по центру
    
    private bool isPlayerFacingRight = true; // служит для отслеживания поворота
    private Vector3 _lookPos;

    void FixedUpdate()
	{
        _lookPos = PlayerLookAround.lookPos;

        if (_lookPos.x < playerPosition.transform.position.x && isPlayerFacingRight)
        {
            offset.x = -offset.x;
            isPlayerFacingRight = false;
        }

        else if (_lookPos.x >= playerPosition.transform.position.x && !isPlayerFacingRight)
        {
            offset.x = -offset.x;
            isPlayerFacingRight = true;
        }

        Vector3 desiredPosition = playerPosition.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
		transform.position = smoothedPosition;
	}
}

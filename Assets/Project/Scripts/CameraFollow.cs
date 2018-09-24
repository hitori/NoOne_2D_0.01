using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform targetToFollow;
	float smoothSpeed = 10f;
	public Vector3 offset;
    public PlayerMovement playerMovementScript;
    Vector3 lookPos;
    bool isPlayerFacingRight = true;

    void FixedUpdate()
	{
        this.lookPos = playerMovementScript.lookPos;

        if (this.lookPos.x + 1.5 < targetToFollow.transform.position.x && isPlayerFacingRight)
        {
            offset.x = -offset.x;
            isPlayerFacingRight = false;
        }

        else if (this.lookPos.x - 1.5 >= targetToFollow.transform.position.x && !isPlayerFacingRight)
        {
            offset.x = -offset.x;
            isPlayerFacingRight = true;
        }


		Vector3 desiredPosition = targetToFollow.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
		transform.position = smoothedPosition;
	}
}

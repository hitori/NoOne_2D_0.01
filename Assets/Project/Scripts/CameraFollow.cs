using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform targetToFollow;
	float smoothSpeed = 10f;
	public Vector3 offset;

	void FixedUpdate()
	{
		Vector3 desiredPosition = targetToFollow.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
		transform.position = smoothedPosition;
	}
}

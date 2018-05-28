using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour 
{
	//for movement
	float movementWalkSpeed = 7f;
	float movementRunSpeed = 15f;
	float movementAmount;
	bool isPlayerFacingRight = true;
	bool isHeadFacingAsBody = true;
	float headRotationSpeed = 10f;
	float fieldOfView = 130f;
	Vector2 velocity;
	Rigidbody2D rb;

	//for sight
	public Transform body;
	public Transform head;


	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
		float movementSpeed;
	}
	

	void Update () 
	{
		PlayerSight ();

		movementAmount = Input.GetAxis ("Horizontal") * movementWalkSpeed;
		if (Input.GetKey (KeyCode.LeftShift)) {
			movementAmount = Input.GetAxis ("Horizontal") * movementRunSpeed;
		}

		if (movementAmount < 0f && isPlayerFacingRight) {
			FlipPlayerBody ();
		} else if (movementAmount > 0f && !isPlayerFacingRight)
			FlipPlayerBody ();
	}

	private void FixedUpdate()
	{
		velocity = rb.velocity;
		velocity.x = movementAmount;
		rb.velocity = velocity; // *time.fixeddeltatime???
	}


	void FlipPlayerBody()
	{
		isPlayerFacingRight = !isPlayerFacingRight;
		body.RotateAround (body.position, Vector3.up, 180);


		// 		FLIP THROUGHT SCALE CHANGING
		//
		//		Vector2 flippedLocalScale = this.transform.localScale;
		//		flippedLocalScale.x *= -1;// THINK ABOUT FLIPPING PLAYER WITH SOME SPEED, NOT JUST INSTATNT
		//		this.transform.localScale = flippedLocalScale;
	}


	void FlipPlayerHead()
	{

	}


	void PlayerSight()
	{
		float angleHB = Vector3.Angle (head.right, body.right);
		if (angleHB < 90)
			isHeadFacingAsBody = true;
		else if (angleHB >= 90)
			isHeadFacingAsBody = false;
		Debug.Log (angleHB);

		Vector2 mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);
		Vector2 direction = mousePosition - new Vector2(head.position.x, head.position.y);
		direction.Normalize ();
		float angle = Vector3.Angle (direction, body.right);

		float rotZ = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
		head.rotation = Quaternion.Euler (0, 0, rotZ);
		if (!isHeadFacingAsBody) {
			head.RotateAround (head.position, Vector3.up, 180);
			isHeadFacingAsBody = true;
		}
			
		



//		if (isHeadFacingAsBody && angle < fieldOfView * 0.5f) {
//			float rotZ = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
//			head.rotation = Quaternion.Euler (0, 0, rotZ);
//		} 
//
//		else if (!isHeadFacingAsBody && angle > 130 && angle < 180) 
//		{
//			head.RotateAround (head.position, Vector3.up, 180);
//		}
	}
}

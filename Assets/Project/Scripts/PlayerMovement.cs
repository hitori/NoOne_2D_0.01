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
	float headRotationSpeed = 10f;
	Vector2 velocity;
	Rigidbody2D rb;

	//for sight
	public GameObject head;


	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
	}
	

	void Update () 
	{
		movementAmount = Input.GetAxis ("Horizontal") * movementWalkSpeed;
		if (Input.GetKey (KeyCode.LeftShift)) {
			movementAmount = Input.GetAxis ("Horizontal") * movementRunSpeed;
		}

		PlayerSight ();
	}

	private void FixedUpdate()
	{
		velocity = rb.velocity;
		velocity.x = movementAmount;
		rb.velocity = velocity;

		if (movementAmount < 0f && isPlayerFacingRight) {
			FlipPlayer ();
		} else if (movementAmount > 0f && !isPlayerFacingRight)
			FlipPlayer ();
	}

	void FlipPlayer()
	{
		isPlayerFacingRight = !isPlayerFacingRight;
		Vector2 flippedLocalScale = this.transform.localScale;
		flippedLocalScale.x *= -1;// THINK ABOUT FLIPPING PLAYER WITH SOME SPEED, NOT JUST INSTATNT
		this.transform.localScale = flippedLocalScale;
	}

	void PlayerSight()
	{
		//main sight controls
//		Vector2 mousePosition = Input.mousePosition;
//		mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);
//		Vector3 direction = new Vector3 (mousePosition.x - head.transform.position.x , mousePosition.y - head.transform.position.y, 0);
//
//
//		if (isPlayerFacingRight)
//			head.transform.right = direction;
//		else
//			head.transform.right = -direction;


		Vector3 mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);
		mousePosition.z = 0;
		Vector3 direction = mousePosition - head.transform.position;
		Quaternion lookRotation = Quaternion.LookRotation (direction);
		Vector3 rotation = Quaternion.Lerp (head.transform.rotation, lookRotation, Time.deltaTime * headRotationSpeed).eulerAngles;
		rotation.z = Mathf.Clamp (rotation.z, 0, 90);
		head.transform.rotation = Quaternion.Euler (0f, 0f, rotation.z);







		//Sight restrictions
//		float angle = Vector3.Angle(direction, transform.right);
//		Mathf.Clamp (angle, 0, 90);
//		Debug.Log (angle);

	}
}

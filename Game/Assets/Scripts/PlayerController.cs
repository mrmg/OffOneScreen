using UnityEngine;
using System.Collections;

public class PlayerController : CharacterBase {

	protected void Update () {
		base.Update();

		float xMove = Input.GetAxis (horizontalControl);
		float zMove = Input.GetAxis (verticalControl);
		float yMove = 0f;

		if (Input.GetButtonDown (jumpControl)) {
			if(currentState == State.Rolling) {
				currentState = State.Jumping;
				yMove = 1;
			} else if(currentState == State.Jumping) {
				Vector3 vel = gameObject.rigidbody.velocity;
				vel = new Vector3(0, -5, 0);
				gameObject.rigidbody.velocity = vel;
				currentState = State.Dropping;
				return;
			}
		}

		gameObject.rigidbody.AddForce( new Vector3 (xMove * moveSpeed, yMove * jumpSpeed, zMove * moveSpeed) );
	}

}

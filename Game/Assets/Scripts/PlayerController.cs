using UnityEngine;
using System.Collections;

public class CharacterBase : MonoBehaviour {
	public bool onGround = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (gameObject.rigidbody.position.y < -6) {
			Debug.Log ("player died");
		}
	}
	
	void OnCollisionEnter (Collision col) {
		if (col.gameObject.name == "Monitor" || col.gameObject.name == "MonitorScreen") {
			onGround = true;
		}
	}
}

public class PlayerController : CharacterBase {
	public float maxSpeed = 5f;
	public float jumpSpeed = 250f;

	public string horizontalControl = "Horizontal";
	public string verticalControl = "Vertical";
	public string jumpControl = "Jump";

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float xMove = Input.GetAxis (horizontalControl);
		float zMove = Input.GetAxis (verticalControl);
		float yMove = 0f;


		if (Input.GetButtonDown (jumpControl) && onGround) {
			onGround = false;
			yMove = 1;
		}

		gameObject.rigidbody.AddForce( new Vector3 (xMove * maxSpeed, yMove * jumpSpeed, zMove * maxSpeed) );
	}

}

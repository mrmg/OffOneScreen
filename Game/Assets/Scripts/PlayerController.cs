using UnityEngine;
using System.Collections;

public class CharacterBase : MonoBehaviour {
	public bool onGround = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float xMove = Input.GetAxis ("Horizontal");
		float zMove = Input.GetAxis ("Vertical");
		float yMove = 0f;


		if (Input.GetKeyDown (KeyCode.Space) && onGround) {
			onGround = false;
			yMove = 1;
		}

		gameObject.rigidbody.AddForce( new Vector3 (xMove * maxSpeed, yMove * jumpSpeed, zMove * maxSpeed) );
	}

}

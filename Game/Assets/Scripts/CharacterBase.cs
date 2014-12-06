using UnityEngine;
using System.Collections;

public class CharacterBase : MonoBehaviour {
	public bool onGround = false;
	public Light spotlight;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update() {
		spotlight.transform.LookAt (gameObject.transform);
	}
	
	void OnCollisionEnter (Collision col) {
		if (col.gameObject.name == "Monitor" || col.gameObject.name == "Screen") {
			onGround = true;
		}
	}
}
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterBase : MonoBehaviour {
	public enum State{Rolling, Jumping, Dropping, Initing};
	public State currentState = State.Rolling;
	public ParticleSystem dropParticle;
	public float moveSpeed = 7.5f;
	public float jumpSpeed = 500f;
	
	public string horizontalControl = "Horizontal";
	public string verticalControl = "Vertical";
	public string jumpControl = "Jump";

	public Text scoreText;

	public enum Control{Player, CPU};
	public Control control;

	protected Vector3 startPosition;

	public int lives = 5;

	void Start () {
		startPosition = transform.position;
		setupStart ();
	}

	public void setupStart(){
		scoreText.gameObject.SetActive (false);
		gameObject.SetActive (false);
	}

	public void ActivatePlayer(Control c) {
		control = c;
		
		scoreText.gameObject.SetActive (true);
		gameObject.SetActive (true);
	}
	
	void OnCollisionEnter (Collision col) {
		if (col.gameObject.name == "Monitor" || col.gameObject.name == "Screen") {
			if(currentState == State.Dropping) {
				Vector3 pos = gameObject.transform.position;
				dropParticle.transform.position = pos;
				dropParticle.Play();
			}
			currentState = State.Rolling;
		}

		if (col.gameObject.name.IndexOf("Player") > -1) {
			if(currentState == State.Dropping || currentState == State.Jumping) {
				// add extra POW!
				col.gameObject.rigidbody.AddForce( new Vector3 (0, 250f, 0) );
			}
		}
	}

	public void resetPosition() {
		lives = 5;
		scoreText.text = "" + lives;
		
		Vector3 pos =  new Vector3(0, 0, 0);
		gameObject.rigidbody.velocity = pos;
		gameObject.transform.position = startPosition;
		currentState = State.Initing;
		setupStart();
	}

	protected void Update() {
		if(transform.position.y < -10) {
			Debug.Log("player died");
			if(--lives < 1) {
				gameObject.SetActive( false );
			} else {
				scoreText.text = "" + lives;
				Vector3 pos =  new Vector3(Random.Range(-3.0f, 3.0f), 0, Random.Range(-3.0f, 3.0f));
				gameObject.transform.position = pos;
				pos.y = 0;
				pos.x = 0;
				pos.z = 0;
				gameObject.rigidbody.velocity = pos;
				currentState = State.Initing;
			}
		}
	}
}
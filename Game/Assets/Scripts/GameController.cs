using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public Camera gameCamera;
	public GameObject screen;

	public enum StateGame{
		Company,
		Title,
		Intro,
		Game,
		GameOver
	}
	public StateGame gameState = StateGame.Company;
	
	public GameObject player1;
	public GameObject player2;
	public GameObject player3;
	public GameObject player4;

	public Texture companyTexture;
	public Texture titleTexture;
	public Texture introTexture;
	public Texture gameTexture;
	public Texture loseTexture;


	// Use this for initialization
	void Start () {
		//StartClicked ();
		iTween.MoveTo(gameCamera.gameObject, iTween.Hash(
			"x", 0,
			"y", 21,
			"z", -1,
			"time",4,
			"delay",1,
			"easetype",iTween.EaseType.linear)
      	);
		iTween.RotateTo(gameCamera.gameObject, iTween.Hash(
			"x", 90,
			"y", 0,
			"z", 0,
			"time",4,
			"delay",1,
			"easetype",iTween.EaseType.linear)
        );

		iTween.ColorTo(screen, iTween.Hash(
			"color", new Color(0, 0, 0),
			"time", 1,
			"delay", 7,
			"oncomplete", "CompanyCompleteHandler",
			"oncompletetarget", gameObject)
		);
	}

	public void CompanyCompleteHandler() {
		
		iTween.ColorTo(screen, iTween.Hash(
			"color", new Color(1, 1, 1),
			"time", 1)
       	);

		gameState = StateGame.Title;

		screen.renderer.material.mainTexture = titleTexture;
	}

	public void StartClicked() {
		
		iTween.ColorTo(screen, iTween.Hash(
			"color", new Color(1, 1, 1),
			"time", 1)
        );
		
		screen.renderer.material.mainTexture = introTexture;
		
		iTween.MoveTo(gameCamera.gameObject, iTween.Hash(
			"x", 0,
			"y", 13,
			"z", -12,
			"time",4,
			"delay",1,
			"easetype",iTween.EaseType.linear)
	    );
		iTween.RotateTo(gameCamera.gameObject, iTween.Hash(
			"x",58.48,
			"y", 0,
			"z", 0,
			"time",4,
			"delay",1,
			"easetype",iTween.EaseType.linear,
			"oncomplete", "StartGame",
			"oncompletetarget", gameObject)
        );

	}

	public void StartGame() {
		CharacterBase script = (CharacterBase) player1.GetComponent (typeof(CharacterBase));
		script.ActivatePlayer(CharacterBase.Control.Player);
		script = (CharacterBase) player2.GetComponent (typeof(CharacterBase));
		script.ActivatePlayer(CharacterBase.Control.CPU);
		script = (CharacterBase) player3.GetComponent (typeof(CharacterBase));
		script.ActivatePlayer(CharacterBase.Control.Player);
		script = (CharacterBase) player4.GetComponent (typeof(CharacterBase));
		script.ActivatePlayer(CharacterBase.Control.CPU);
		
		
		iTween.ColorTo(screen, iTween.Hash(
			"color", new Color(0, 0, 0),
			"time", 1,
			"delay", 0)
       	);


		gameState = StateGame.Game;
	}

	protected void Update() {
		if (gameState == StateGame.Title && Input.GetMouseButtonDown (0)) {
			// check mouse position

			float posX = (Input.mousePosition.x / Screen.width)*100;
			float posY = (Input.mousePosition.y / Screen.height)*100;

			bool goStart = false;

			if(posY > 32.5 && posY < 40) {
				if(posX > 22 && posX < 36) {
					goStart = true;
				} else if(posX > 61 && posX < 75.5) {
					goStart = true;
				}
			}

			if(goStart) {
				
				gameState = StateGame.Intro;
				iTween.ColorTo(screen, iTween.Hash(
					"color", new Color(0, 0, 0),
					"time", 1,
					"delay", 0,
					"oncomplete", "StartClicked",
					"oncompletetarget", gameObject)
                );
			}
		} else if(gameState == StateGame.Game) {
			CharacterBase script = (CharacterBase) player1.GetComponent (typeof(CharacterBase));
			int lives1 = script.lives;
			script = (CharacterBase) player2.GetComponent (typeof(CharacterBase));
			int lives2 = script.lives;
			script = (CharacterBase) player3.GetComponent (typeof(CharacterBase));
			int lives3 = script.lives;
			script = (CharacterBase) player4.GetComponent (typeof(CharacterBase));
			int lives4 = script.lives;

			int deadCount = 0;
			if(lives1 == 0) deadCount++;
			if(lives2 == 0) deadCount++;
			if(lives3 == 0) deadCount++;
			if(lives4 == 0) deadCount++;

			if(deadCount == 3) {
				script = (CharacterBase) player1.GetComponent (typeof(CharacterBase));
				script.resetPosition();
				script = (CharacterBase) player2.GetComponent (typeof(CharacterBase));
				script.resetPosition();
				script = (CharacterBase) player3.GetComponent (typeof(CharacterBase));
				script.resetPosition();
				script = (CharacterBase) player4.GetComponent (typeof(CharacterBase));
				script.resetPosition();
				
				screen.renderer.material.mainTexture = loseTexture;
				
				iTween.ColorTo(screen, iTween.Hash(
					"color", new Color(1, 1, 1),
					"time", 1,
					"delay", 0)
                );

				gameState = StateGame.GameOver;
			}

		} else if(gameState == StateGame.GameOver && Input.GetMouseButtonDown (0)) {
			
			iTween.MoveTo(gameCamera.gameObject, iTween.Hash(
				"x", 0,
				"y", 21,
				"z", -1,
				"time",2,
				"delay",0,
				"easetype",iTween.EaseType.linear)
      		);
			iTween.RotateTo(gameCamera.gameObject, iTween.Hash(
				"x", 90,
				"y", 0,
				"z", 0,
				"time",2,
				"delay",0,
				"easetype",iTween.EaseType.linear)
        	);
			
			iTween.ColorTo(screen, iTween.Hash(
				"color", new Color(0, 0, 0),
				"time", 1,
				"delay", 3,
				"oncomplete", "CompanyCompleteHandler",
				"oncompletetarget", gameObject)
       		);
		}
	}

}

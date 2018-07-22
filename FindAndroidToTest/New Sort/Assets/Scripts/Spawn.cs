using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Spawn : MonoBehaviour {
	public static System.Random rnd = new System.Random();
	public float delay = 2f;
	private float timer;
	public GameObject CircleSprite;
	public GameObject TextPrefab;
	public GameObject WrongPrefab;
	public GameObject x2powerup;
	public GameObject Background;
	//public SpriteRenderer Rend;

	public Texture2D playPic;
	public Texture2D tutorialButton;
	public Texture2D playAgain;
	public Canvas NumCanvas;
	private double[] columns = {-2.5, -1.25,0, 1.25, 2.5};
	public int colLen = 5;

	int lastSpawnedColumn = 0;
	public List<int> inPlay = new List<int>();
	public List<GameObject> objectsInPlay = new List<GameObject>();
	public List<GameObject> powerupsInPlay = new List<GameObject>();


	public AudioClip pop;
	public AudioClip miss;
	public AudioClip ground;
	public AudioClip laser;

	public Text title;
	public Text score;
	public Text GameOverText;
	public Text countText;
	public Text highscore;
	public Text GlobalHighText;
    public Text currentScore;
	public Text instructions;
	public bool gameOn = false;
	public bool tutorial = false;
	public bool showbutton = true;
	public bool gameOver = false;
	public bool invert = false;

	//public Texture backgroundTexture;
	public float guiPlacementX1;
	public float guiPlacementY1;


	public float guiPlacementX2;
	public float guiPlacementY2;

	public float guiPlacementX3;
	public float guiPlacementY3;
	public int HEALTH = 0; // health
	public int maxScore = 0; // highest score achieved
	public int GlobalMax = 0;
	public int mySize = 45;
	public int tutorialsize = 35;
	public int beginning = 0;


	public float x1 = .35f;
	public float x2 = .1f;
	public float x3 = .4f;
	public 	float x4 = .1f;
	public int doubleup = 1;
	public int count = 0;

		void Start () {
		
		Screen.orientation = ScreenOrientation.Portrait;		
		//InvokeRepeating ("Spawnn", delay, delay);

	


	}


	// Update is called once per frame

	void Spawnn () {

		if (!gameOver) {
			int randomColumn = Random.Range (0, 4);
			if (randomColumn == lastSpawnedColumn) 
			{
				randomColumn = randomColumn + 1 % colLen;
			}
			lastSpawnedColumn = randomColumn;

			GameObject circle = Instantiate(CircleSprite, new Vector3((float)(columns[randomColumn]),5,0),Quaternion.identity);
			GameObject text = Instantiate(TextPrefab, NumCanvas.transform);



			int num = rnd.Next(beginning, beginning+15);
			beginning = num - 7;
			if (beginning < 0) {
				beginning = 0;
			}
	

			int powerup = rnd.Next (0, 10);
			if (powerup == 3) {
				if (powerupsInPlay.Count == 0) {
					GameObject timesTwo = Instantiate(x2powerup, new Vector3((float)(columns[(randomColumn+1)%colLen]),5,0),Quaternion.identity);
					powerupsInPlay.Add (timesTwo);

				}
			}


			inPlay.Add (num);
			objectsInPlay.Add (circle);
			inPlay.Sort ();
			//Debug.Log ("index 0 : " + inPlay [0]);

			text.GetComponent<Text> ().text = num.ToString();
			circle.GetComponent<AttachNum> ().num = text.GetComponent<Text> ();
		
		}
	}

void Update(){
		if (gameOn && inPlay.Count == 1) {
			Spawnn ();
		}

		if (invert)
			Background.transform.GetChild (0).GetComponent<SpriteRenderer> ().enabled = true;
		else
			Background.transform.GetChild (0).GetComponent<SpriteRenderer> ().enabled = false;
		



		if (HEALTH >= 1000) {
			tutorial = false;
			instructions.text = "";
		}
			
		List<GameObject> removeBecauseOffScreen = new List<GameObject> ();
		List<GameObject> removeBecauseOffScreen2 = new List<GameObject> ();

		foreach (GameObject pu in powerupsInPlay) {
			if (pu.transform.position.y <= -4.5) {
				removeBecauseOffScreen2.Add (pu);
			}


			if (maxScore > 6500) {
				delay = 1f;
				pu.GetComponent<Move> ().TimeToReachBottom =6.0f;

			}
			if (maxScore > 10000) {
				delay = .7f;
				pu.GetComponent<Move> ().TimeToReachBottom =5.0f;

			}
			if (maxScore > 13500) {
				pu.GetComponent<Move> ().TimeToReachBottom =4.4f;

			}
			if (maxScore > 15000) {
				pu.GetComponent<Move> ().TimeToReachBottom =3.9f;

			}

			if (maxScore > 16000) {
				pu.GetComponent<Move> ().TimeToReachBottom =2.5f;

			}

		}

		for (int i = 0; i < removeBecauseOffScreen2.Count; i++) 
		{
			GameObject po1 = removeBecauseOffScreen2[i];
			powerupsInPlay.Remove (po1);
			GameObject.Destroy (po1);
		}
	
		foreach (GameObject go1 in objectsInPlay) {
			if (maxScore > 700) {
				delay = 1.8f;
			}
			if (maxScore > 6500) {
				delay = 1f;
				go1.GetComponent<Move> ().TimeToReachBottom =6.0f;

			}
			if (maxScore > 10000) {
				delay = .7f;
				go1.GetComponent<Move> ().TimeToReachBottom =5.0f;

			}
			if (maxScore > 13500) {
				go1.GetComponent<Move> ().TimeToReachBottom =4.4f;

			}
			if (maxScore > 15000) {
				go1.GetComponent<Move> ().TimeToReachBottom =3.9f;

			}
			if (maxScore > 16000) {
				go1.GetComponent<Move> ().TimeToReachBottom =2.5f;

			}

			if (go1.transform.position.y <= -4.5) {
				removeBecauseOffScreen.Add (go1);
				inPlay.Remove (int.Parse (go1.GetComponent<AttachNum>().num.text));
				SoundManager.instance.PlaySingle (ground);
				inPlay.Sort ();
				if (!tutorial) {
					if (maxScore <= 3000) {
						HEALTH -= 100;
					}
					if (maxScore > 3000 && maxScore < 12500) {
						HEALTH -= 200;
					} 
					if (maxScore >=12500){
						HEALTH -= 500;
					}
					if (maxScore >= 15000) {
						HEALTH -= 1000;
					}
				}
                UpdateScore(HEALTH);    // subtract health when object falls off screen
				if(HEALTH <= 0 && !tutorial){
                    currentScore.text = "";
                    EndGame();
				}
			}
		}

		for (int i = 0; i < removeBecauseOffScreen.Count; i++) 
		{
			GameObject go1 = removeBecauseOffScreen[i];
			objectsInPlay.Remove (go1);
			Destroy (go1.GetComponent<AttachNum> ().num);
			GameObject.Destroy (go1);
		}
		removeBecauseOffScreen.Clear ();
		removeBecauseOffScreen2.Clear ();
				

		if (!gameOver) {
			timer += Time.deltaTime;
			if (timer >= 10f) {
				doubleup = 1;
				invert = false;
			}
		

			foreach(Touch t in Input.touches) // for each touch in the game
			{

				//Debug.Log ("registered touch at " + t.position);

				if (t.phase == TouchPhase.Began) { // if the touch just began
					var worldPoint = Camera.main.ScreenToWorldPoint (t.position); // point
					GameObject toDestroy = null;
					GameObject toDestroy2 = null;



					//foreach (GameObject pu in powerupsInPlay) { // compare to all gameobjects which are your powerups
					if(powerupsInPlay.Count >= 1){
						GameObject pu = powerupsInPlay [0];
						SpriteRenderer render = pu.GetComponent<SpriteRenderer> ();
						Vector2 TL = new Vector2 (pu.transform.position.x - render.bounds.size.x / 2, 
							             pu.transform.position.y - render.bounds.size.x / 2); // top left of each GO
						Vector2 BR = new Vector2 (pu.transform.position.x + render.bounds.size.y / 2, 
							             pu.transform.position.y + render.bounds.size.y / 2); // bottom right of each game object

						if (isColliding (TL, BR, worldPoint)) { 
							toDestroy2 = pu; // set up to destroy
						}
					}

					if (toDestroy2 != null) {
						timer = 0;
						doubleup = 2;
						invert = true;
						SoundManager.instance.PlaySingle (laser);
						powerupsInPlay.RemoveAt (0);
						Destroy (toDestroy2); // destroy it!
					}

					foreach (GameObject go in objectsInPlay) { // compare to all gameobjects which are your cirlces
						SpriteRenderer render = go.GetComponent<SpriteRenderer> ();
						Vector2 TL = new Vector2 (go.transform.position.x - render.bounds.size.x / 2, 
							go.transform.position.y - render.bounds.size.x / 2); // top left of each GO
						Vector2 BR = new Vector2 (go.transform.position.x + render.bounds.size.y / 2, 
							go.transform.position.y + render.bounds.size.y / 2); // bottom right of each game object
						
						if (isColliding(TL, BR, worldPoint)) { 
							toDestroy = go; // set up to destroy
						}
					}
					if (toDestroy != null){ // if object is clicked on
						// if inPlay.indexOf(toDestroy.number) == 0
						if (inPlay.IndexOf (int.Parse (toDestroy.GetComponent<AttachNum> ().num.text)) == 0) {
							//lowest number
							SoundManager.instance.PlaySingle(pop);
							toDestroy.GetComponent<Move> ().StartDisplayPop ();
							inPlay.RemoveAt(0);
							Destroy(toDestroy.GetComponent<AttachNum>().num);
							Destroy (toDestroy); // destroy it!
							objectsInPlay.Remove(toDestroy);
							inPlay.Sort ();

							HEALTH += 100 * doubleup;		// gives points for correctly popping bubble
							count+=1;

	                        UpdateScore(HEALTH);
							if (maxScore < HEALTH) {
								maxScore = HEALTH;
							}
							if (GlobalMax < maxScore) {
								GlobalMax = maxScore;
								GlobalHighText.text = GlobalMax.ToString();
							}
						} 
							else {
							Handheld.Vibrate ();
							toDestroy.GetComponent<Move> ().StartDistplayX ();
							SoundManager.instance.PlaySingle (miss);
							//HEALTH -= 100; // subtract health
							// then check up on the health
							//if(HEALTH <= 0){
							//	EndGame ();
							}
						}
					}
				}
			}
		}




	bool isColliding (Vector2 TL, Vector2 BR, Vector2 point){
		if (point.x > BR.x || point.x < TL.x) 
		{
			return false;
		}
		if (point.y > BR.y || point.y < TL.y) 
		{
			return false;
		}
		return true;
	}

	void OnGUI(){
		//GUI.DrawTexture(new Rect(0,0, Screen.width, Screen.height), backgroundTexture);


		GUIStyle myStyle = new GUIStyle();
		GUIStyle TutorialStyle = new GUIStyle();
		TutorialStyle.fontSize = tutorialsize;

		myStyle.fontSize = mySize;
	
					
		if (showbutton) {
			title.text = "Sort'Num";
			//if (GUI.Button (new Rect (Screen.width * guiPlacementX1, Screen.height * guiPlacementY1, Screen.width * .5f, Screen.height * .1f), "Play", myStyle)) {
			GUI.backgroundColor = new Color(0,0,0,0);
			if (GUI.Button (new Rect (Screen.width * guiPlacementX1, Screen.height * guiPlacementY1, Screen.width * x1, Screen.height * x2), playPic)) {
				UpdateScore(0);
				SoundManager.instance.PlaySingle (pop);
				SoundManager.instance.musicSource.Stop ();
				title.text = "";
				gameOn = true;
				showbutton = false;
				InvokeRepeating ("Spawnn", delay, delay);

			}
		}

		if (showbutton) {
			//if (GUI.Button (new Rect (Screen.width * guiPlacementX2, Screen.height * guiPlacementY2, Screen.width * .4f, Screen.height * .1f), "Play Tutorial", TutorialStyle)) {
			GUI.backgroundColor = new Color(0,0,0,0);
			if (GUI.Button (new Rect (Screen.width * guiPlacementX2, Screen.height * guiPlacementY2, Screen.width * x3, Screen.height * x4), tutorialButton)) {
				
				SoundManager.instance.PlaySingle (pop);
				SoundManager.instance.musicSource.Stop ();
				UpdateScore(0);
				title.text = "";
				gameOn = true;
				instructions.text = "Tap on the numbers from least to greatest!\n" +
					" \nDon't let them hit the ground!"; 
				showbutton = false;
				tutorial = true;
				InvokeRepeating ("Spawnn", delay, delay);


		
			}
		}
		if (gameOver && inPlay.Count == 0) {
			//if (GUI.Button (new Rect (Screen.width * guiPlacementX3, Screen.height * guiPlacementY3, Screen.width * .5f, Screen.height * .1f), "Play Again", myStyle)) {
			GUI.backgroundColor = new Color(0,0,0,0);
			if (GUI.Button (new Rect (Screen.width * guiPlacementX3, Screen.height * guiPlacementY3, Screen.width * .5f, Screen.height * .1f), playAgain)) {
				
				UpdateScore(0);
				HEALTH = 0;
				maxScore = 0;
				count = 0;
				gameOn = true;
				gameOver = false;
				GameOverText.text = "";
				highscore.text = "";
				score.text = "";
				countText.text = ""; 

				SoundManager.instance.musicSource.Stop ();
				//InvokeRepeating ("Spawnn", delay, delay);
			}
		}


	}

	void EndGame(){
		Debug.Log ("Game Over"); // eventually put in Game over screen
		gameOver = true;
		GameOverText.text = "Game Over";
		highscore.text = "High Score";
		countText.text = "You Hit: " + count + " Numbers"; 

		if(!SoundManager.instance.musicSource.isPlaying)
			SoundManager.instance.musicSource.Play ();
		score.text = maxScore.ToString ();

		showbutton = false;
		gameOn = false;

	}

    void UpdateScore(int score) {

        currentScore.text = " Score: " + score.ToString();


    }
	
}


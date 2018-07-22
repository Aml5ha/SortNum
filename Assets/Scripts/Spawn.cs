using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Spawn : MonoBehaviour {
	public static System.Random rnd = new System.Random();
	public float delay = 1f;
	public GameObject CircleSprite;
	public GameObject TextPrefab;
	public Canvas NumCanvas;
	private double[] columns = {-2.5, -1.25,0, 1.25, 2.5};
	public List<int> inPlay = new List<int>();
	public List<GameObject> objectsInPlay = new List<GameObject>();

	public Text title;
	public Text score;
	public Text GameOverText;
	public Text highscore;
    public Text currentScore;
	public bool gameOn = false;
	public bool skip = false;
	public bool showbutton = true;
	public bool gameOver = false;

	//public Texture backgroundTexture;
	public float guiPlacementX1;
	public float guiPlacementY1;


	public float guiPlacementX2;
	public float guiPlacementY2;
	public int HEALTH = 0; // health
	public int maxScore = 0; // highest score achieved

		void Start () {

        UpdateScore(0);
		//InvokeRepeating ("Spawnn", delay, delay);
	


	}


	// Update is called once per frame

	void Spawnn () {
		if (!gameOver) {

			GameObject circle = Instantiate(CircleSprite, new Vector3((float)(columns[Random.Range(0,4)]),5,0),Quaternion.identity);
			GameObject text = Instantiate(TextPrefab, NumCanvas.transform);
			int num = rnd.Next(0, 101);


			inPlay.Add (num);
			objectsInPlay.Add (circle);
			inPlay.Sort ();
			//Debug.Log ("index 0 : " + inPlay [0]);

			text.GetComponent<Text> ().text = num.ToString();
			circle.GetComponent<AttachNum> ().num = text.GetComponent<Text> ();
		
		}
	}

void Update(){
		
		List<GameObject> removeBecauseOffScreen = new List<GameObject> ();
		foreach (GameObject go1 in objectsInPlay) {
			if (go1.transform.position.y <= -5) {
				removeBecauseOffScreen.Add (go1);
				inPlay.Remove (int.Parse (go1.GetComponent<AttachNum>().num.text));
				inPlay.Sort ();
				HEALTH -= 200;
                UpdateScore(HEALTH);    // subtract health when object falls off screen
				if(HEALTH <= 0){
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
				

		if (!gameOver) {
			foreach(Touch t in Input.touches) // for each touch in the game
			{

				Debug.Log ("registered touch at " + t.position);

				if (t.phase == TouchPhase.Began) { // if the touch just began
					var worldPoint = Camera.main.ScreenToWorldPoint (t.position); // point
					GameObject toDestroy = null;
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
							inPlay.RemoveAt(0);
							Destroy(toDestroy.GetComponent<AttachNum>().num);
							Destroy (toDestroy); // destroy it!
							objectsInPlay.Remove(toDestroy);
							inPlay.Sort ();

							HEALTH += 100;		// gives points for correctly popping bubble
                            UpdateScore(HEALTH);
							if (maxScore < HEALTH) {
								maxScore = HEALTH;
							}
						} 
							//else {
							//HEALTH -= 100; // subtract health
							// then check up on the health
							//if(HEALTH <= 0){
							//	EndGame ();
							//}
						//}
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

		if (showbutton) {
			title.text = "Sort'Num";
			if (GUI.Button (new Rect (Screen.width * guiPlacementX1, Screen.height * guiPlacementY1, Screen.width * .5f, Screen.height * .1f), "Play")) {
				print ("Clicked");
				title.text = "";
				gameOn = true;
				showbutton = false;
				InvokeRepeating ("Spawnn", delay, delay);
			}
		}

		if (!showbutton && !skip) {
			if (GUI.Button (new Rect (Screen.width * guiPlacementX2, Screen.height * guiPlacementY2, Screen.width * .1f, Screen.height * .05f), "Skip")) {
				print ("Skip");
				showbutton = false;
				skip = true;
		
			}
		}
		if (gameOver) {
			if (GUI.Button (new Rect (Screen.width * guiPlacementX1, Screen.height * guiPlacementY1, Screen.width * .5f, Screen.height * .1f), "Play Again?")) {
				gameOn = true;
				gameOver = false;
				GameOverText.text = "";
				highscore.text = "";
				score.text = "";

				//InvokeRepeating ("Spawnn", delay, delay);
			}
		}


	}

	void EndGame(){
		Debug.Log ("Game Over"); // eventually put in Game over screen
		gameOver = true;
		GameOverText.text = "Game Over";
		highscore.text = "High Score";
		score.text = maxScore.ToString ();
		showbutton = false;
		gameOn = false;

	}

    void UpdateScore(int score) {

        currentScore.text = " Score: " + score.ToString();


    }
	


}

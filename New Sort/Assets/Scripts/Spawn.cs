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


		void Start () {
		InvokeRepeating ("Spawnn", delay, delay);


	}


	// Update is called once per frame

	void Spawnn () {

		GameObject circle = Instantiate(CircleSprite, new Vector3((float)(columns[Random.Range(0,4)]),5,0),Quaternion.identity);
		GameObject text = Instantiate(TextPrefab, NumCanvas.transform);
		int num = rnd.Next(0, 101);


		inPlay.Add (num);
		inPlay.Sort ();

		//Debug.Log ("index 0 : " + inPlay [0]);

		text.GetComponent<Text> ().text = num.ToString();
		circle.GetComponent<AttachNum> ().num = text.GetComponent<Text> ();
	}


}

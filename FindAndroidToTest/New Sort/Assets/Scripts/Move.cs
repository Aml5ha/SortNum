using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
	public float TimeToReachBottom = 7.0f;
	private float timer;
	private Vector3 position;
	public Vector3 target;

	public AnimationCurve animCurve;

//	private float xTimer;
//	public float MAX_X_TIME = 3;


	// Use this for initialization
	void Start () {

		//speed = Random.Range (.1f, 1f);
		timer = 0;
		float x = transform.position.x;
		position = this.transform.position;
		target = new Vector3 (x, -5, 0); 
//		position = CircleSprite.transform.position;
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		float ratio = timer / TimeToReachBottom;
		float yRatio = animCurve.Evaluate (ratio);
		float yPosition = Mathf.Lerp (position.y, target.y, yRatio);
		transform.position = new Vector3 (position.x, yPosition, position.z);
	}

	public void StartDistplayX()
	{
		//xTimer = 0;
		this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
		StopAllCoroutines ();
		StartCoroutine (DisplayX());
	}

	public IEnumerator DisplayX()
	{
		//xTimer += Time.deltaTime;
//		while (xTimer < MAX_X_TIME) {
//			yield return null;
//		}
		yield return new WaitForSecondsRealtime(.8f);
		this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
	}

	public void StartDisplayPop() {
		this.gameObject.transform.GetChild (1).GetComponent<SpriteRenderer>().enabled = true;
		StopAllCoroutines ();
		StartCoroutine (DisplayPop ());
	}

	public IEnumerator DisplayPop() {
		yield return new WaitForSecondsRealtime (.5f);
		this.gameObject.transform.GetChild (1).GetComponent<SpriteRenderer> ().enabled = false;
	}
}



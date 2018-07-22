using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
	public float speed = .8f;
	private Vector3 position;
	public Vector3 target;


	// Use this for initialization
	void Start () {

		//speed = Random.Range (.1f, 1f);
		float x = transform.position.x;
		target = new Vector3 (x, -5, 0); 
//		position = CircleSprite.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target, step);
	}
		


}



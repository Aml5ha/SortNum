using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AttachNum : MonoBehaviour  {
	//Spawn spawner = new Spawn();

	public Text num;

	// Update is called once per frame
	void Update () {

		num.transform.position = this.transform.position;
//		if (transform.position.y <= -5) {
//			spawner.inPlay.Remove (int.Parse(num.text));
//
//			Destroy (gameObject);
//			Destroy (num);
//		}
	}


}

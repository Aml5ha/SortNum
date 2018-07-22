using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AttachNum : MonoBehaviour {

	public Text num;

	// Update is called once per frame
	void Update () {

		num.transform.position = this.transform.position;
		if (transform.position.y <= -5) {
			Destroy (gameObject);
			Destroy (num);
		}
	}
}

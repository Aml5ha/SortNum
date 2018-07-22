using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

	private Vector3 touchOrigin = -Vector3.one;
	private Vector3 touchPosition;

	#if UNITY_STANDALONE || UNITY_WEBPLAYER

	void OnMouseDown(){
		Destroy (gameObject);

	}

	#else

	void touch(){
		if(Input.touchCount > 0) {
			touchPosition = Camera.main.ScreenToWorldPoint (Input.GetTouch(0).position);
			Vector3 touchPositionVector = new Vector3 (touchPosition.x, touchPosition.y);
			RaycastHit2D hitInformation = Physics2D.Raycast (touchPositionVector, Camera.main.transform.forward);
			if (hitInformation.collider != null) {
				Destroy (hitInformation.transform.gameObject);
			}
		}
	}

	#endif
}

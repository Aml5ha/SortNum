using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spheres : MonoBehaviour {

	public float delay = 0.3f;
	public GameObject sphere;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", delay, delay);
		Physics.gravity = new Vector3 (0, - 0.5F, 0); //speed of fall
	}

	void Spawn () {
		Instantiate(sphere, new Vector3(Random.Range(-6,6),10,0),Quaternion.identity);
	}
	// Deletes stuff from the world

}

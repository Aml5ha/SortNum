using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioSource efxSource;
	public AudioSource musicSource;
	public static SoundManager instance = null;

	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);
		
		DontDestroyOnLoad (gameObject);
	}

	public void PlaySingle(AudioClip clip) {
		efxSource.clip = clip;
		efxSource.Play ();
	}
		
}

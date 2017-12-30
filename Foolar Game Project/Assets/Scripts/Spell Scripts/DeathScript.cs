using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour {

	public int lifeSpan;
	public GameObject deathAnimation;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (lifeSpan-- <= 0) {
			if(deathAnimation != null)
				Instantiate (deathAnimation, gameObject.transform.position, gameObject.transform.rotation);
			Destroy (gameObject);
		}
	}
}

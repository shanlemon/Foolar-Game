using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

	public float moveSpeed, acc, lifeSpan;
	public float power;

	void Start () {	
		
	}

	void Update () {
		gameObject.transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
		moveSpeed += acc;
		if (lifeSpan-- <= 0) {
			delete ();
		}
	}

	public void delete() {
		Destroy (gameObject);
	}

	void OnTriggerEnter(Collider other) {
		GameObject obj = other.gameObject;
		if (obj.tag.Equals ("Ball")) {
			obj.GetComponent<BallController> ().push (power, transform);
		}
		Debug.Log ("xd");
		delete ();
	}
}

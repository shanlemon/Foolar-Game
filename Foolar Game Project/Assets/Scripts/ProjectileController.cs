using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

	public float moveSpeed, acc;
	public float power;
	public Rigidbody rb;

	void Start () {	
		//rb = GetComponent<Rigidbody> ();
	}

	void Update () {
		//rb.AddForce (Vector3.forward * moveSpeed * Time.deltaTime);
		gameObject.transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
		moveSpeed += acc;
	}

	public void delete() {
		Destroy (gameObject);
	}

	void OnTriggerEnter(Collider other) {
		GameObject obj = other.gameObject;
		if (obj.tag.Equals ("Ball")) {
			obj.GetComponent<BallController> ().push (power, transform);
		}
		delete ();
		Debug.Log ("xd");
	}
}

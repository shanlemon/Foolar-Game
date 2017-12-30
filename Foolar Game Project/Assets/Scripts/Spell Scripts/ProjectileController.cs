using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

	public float moveSpeed, acc;
	public float power;
	public Rigidbody rb;

	void Start () {	
		rb = GetComponent<Rigidbody>();

	}

    void FixedUpdate() {
        rb.MovePosition(transform.position + (moveSpeed * transform.forward * Time.deltaTime));
        moveSpeed += acc;
    }

	public void delete() {
		Destroy (gameObject);
	}

	void OnTriggerEnter(Collider other) {
		GameObject obj = other.gameObject;
        if (obj.tag.Equals("Ball")) {
            if (this.gameObject.name == "Fireball(Clone)") {
                obj.GetComponent<BallController>().push(power, transform);
            } else if (this.gameObject.name == "IceBall(Clone)") {
                BallController bc = obj.GetComponent<BallController>();
                bc.StartCoroutine(bc.freezeBall(1));
            }

        }
        delete ();
	}
}

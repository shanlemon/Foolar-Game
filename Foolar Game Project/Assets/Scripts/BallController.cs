using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	void Update () {
		
	}

	public void push (float power, Transform trans) {
		Vector3 shootDir = transform.position - trans.position; //calculate delta vector
		shootDir.Normalize (); //normalize
		rb.AddForce (shootDir * (power / 5000000), ForceMode.Impulse);
	}

    public IEnumerator freezeBall(float time) {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(time);
        rb.constraints = RigidbodyConstraints.None;
    }


}

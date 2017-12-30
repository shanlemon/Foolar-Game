using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

    public void OnCollisionEnter(Collision collision) {
        rb.constraints = RigidbodyConstraints.None;
    }


    public void push (float power, Transform trans) {
        //Vector3 shootDir = transform.position - trans.position; //calculate delta vector
        //shootDir.Normalize (); //normalize

        Vector3 shootDir = trans.GetComponent<Rigidbody>().velocity.normalized;

        rb.constraints = RigidbodyConstraints.None;

        rb.AddForce (shootDir * (power *.00000022222f), ForceMode.Impulse);
	}

    public IEnumerator freezeBall(float time) {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(time);
        rb.constraints = RigidbodyConstraints.None;
    }


}

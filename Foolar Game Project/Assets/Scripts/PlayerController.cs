using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    private Vector3 movement;
    public float movementSpeed = 10f;
    public float rotateSpeed = 150f;
    public float jumpForce;
    private bool isGrounded;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
		
	}
	
	// Update is called once per frame
	void Update () {

        movement = Vector3.forward * movementSpeed * Input.GetAxis("Vertical");
        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime ,0));
        transform.Translate(movement * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Space)) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 2)) {
                rb.velocity = (Vector3.up * Time.deltaTime * jumpForce);
            }
        }

    }

}

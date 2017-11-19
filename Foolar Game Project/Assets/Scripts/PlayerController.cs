using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody rb;
    private Vector3 movement;
    public float movementSpeed = 10f;
    public float rotateSpeed = 150f;
	public float rotationSpeed = 2.5f;
    public float jumpForce;
	public Camera cam;
	private bool isGrounded;
	public Spells[] spell;
	public KeyCode[] keys;
	public CameraController cameraController;

    // Use this for initialization
    void Start () {
        //rb = GetComponent<Rigidbody>();

	}

	// Update is called once per frame
	void Update () {
		Debug.Log (cameraController.isBallFocused);
          if (cameraController.isBallFocused) {
              movement = Vector3.forward * Input.GetAxis("Vertical") * movementSpeed;
              transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed, 0, Space.Self);
          } else {
              movement = new Vector3(movementSpeed * Input.GetAxis("Horizontal"), 0, movementSpeed * Input.GetAxis("Vertical"));
          }

		//Free Look if alt pressed
		if (!Input.GetKey (KeyCode.LeftAlt) && !cameraController.isBallFocused) {
			transform.eulerAngles = new Vector3 (0, cam.transform.eulerAngles.y);
		}

		//rb.AddForce (transform.forward * 20, ForceMode.Impulse);
		transform.Translate (movement * Time.deltaTime);

		//Jumping
		if (Input.GetKeyDown (KeyCode.Space)) {
			RaycastHit hit;
			if (Physics.Raycast (transform.position, Vector3.down, out hit, 2)) {
				rb.velocity = (Vector3.up * Time.deltaTime * jumpForce);
			}
		}

		//running
		movementSpeed = (Input.GetKey (KeyCode.LeftShift)) ? 50f : 10f;

		for (int i = 0; i < keys.Length; i++) {
			if (Input.GetKeyDown (keys [i])) {
				spell [i].cast ();
			}
		}
	}

}

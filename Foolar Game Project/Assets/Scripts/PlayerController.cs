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
    public int castingIndex;
    public bool isCasting;

    // Use this for initialization
    void Start () {
        //rb = GetComponent<Rigidbody>();

	}

	// Update is called once per frame
	void Update () {
          if (cameraController.isBallFocused) {
              movement = Vector3.forward * Input.GetAxis("Vertical") * movementSpeed;
              transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed, 0, Space.Self);
          } else {
              movement = new Vector3(movementSpeed * Input.GetAxis("Horizontal"), 0, movementSpeed * Input.GetAxis("Vertical"));
          }

        if (isCasting) {

            //if left click
            if (Input.GetMouseButtonDown(0)) {
                spell[castingIndex].cast();
                spell[castingIndex].deleteHologram();
                castingIndex = -1;
                isCasting = false;

            }
            //if right click
            else if (Input.GetMouseButtonDown(1)) {
                spell[castingIndex].deleteHologram();
                castingIndex = -1;
                isCasting = false;
            }

            //showHologram
            else {
                if (!spell[castingIndex].showingHologram) {
                    spell[castingIndex].showHologram();
                } else {
                    spell[castingIndex].updateHologram();
                }
            }

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

        if (!cameraController.isBallFocused) {
            for (int i = 0; i < keys.Length; i++) {
                if (Input.GetKeyDown(keys[i])) {
                    //spell [i].cast ();
                    if (spell[i].canCast()) {
                        if (isCasting == true) {
                            spell[castingIndex].deleteHologram();
                        }
                        castingIndex = i;
                        if (spell[castingIndex].quickCast) {
                            spell[castingIndex].cast();
                            spell[castingIndex].deleteHologram();
                            castingIndex = -1;
                            isCasting = false;
                        }else {
                            isCasting = true;
                        }
                    }
                }
            }
        }
	}

        

}

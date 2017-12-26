 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Components
    public Rigidbody rb;
    public Camera cam;
    public CameraController cameraController;

    //movement
    private Vector3 movement;
    public float movementSpeed = 10f;
    public float rotationSpeed = 2.5f;

    //jumping
    public float jumpForce;
    private bool isGrounded;

    //spells
    public Spells2[] spell;
    public KeyCode[] keys;
    public int castingIndex;
    public bool isCasting;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        if (!GameController.settingsOpen) {
            if (cameraController.isBallFocused) {
                movement = Vector3.forward * Input.GetAxis("Vertical") * movementSpeed;
                transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed, 0, Space.Self);
            } else {
                movement = new Vector3(movementSpeed * Input.GetAxis("Horizontal"), 0, movementSpeed * Input.GetAxis("Vertical"));
            }

            //Free Look if alt pressed
            if (!Input.GetKey(KeyCode.LeftAlt) && !cameraController.isBallFocused) {
                transform.eulerAngles = new Vector3(0, cam.transform.eulerAngles.y);
            }

            //rb.AddForce (transform.forward * 20, ForceMode.Impulse);
            transform.Translate(movement * Time.deltaTime);

            //Jumping
            if (Input.GetKeyDown(KeyCode.Space)) {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, Vector3.down, out hit, 2)) {
                    rb.velocity = (Vector3.up * Time.deltaTime * jumpForce);
                }
            }

            //running
            movementSpeed = (Input.GetKey(KeyCode.LeftShift)) ? 50f : 10f;

            //Casting
            if (!cameraController.isBallFocused) {
                for (int i = 0; i < keys.Length; i++) {
                    if (Input.GetKeyDown(keys[i])) {
                        if (spell[i].canCast()) {
                            if (spell[i].hologramPrefab != null) {
                                isCasting = true;
                                castingIndex = i;
                                spell[i].showHologram();
                            } else {
                                if (isCasting) {
                                    stopHologram();
                                }
                                spell[i].cast(Spells2.InputSent.keyboard);
                            }
                        }
                    }
                }
                if (isCasting) {
                    if (Input.GetMouseButtonDown(0)) {
                        spell[castingIndex].cast(Spells2.InputSent.leftClick);
                        stopHologram();

                    }
                     //if right click
                     else if (Input.GetMouseButtonDown(1)) {
                        stopHologram();
                    }

                     //showHologram
                     else {
                        spell[castingIndex].updateHologram();
                    }
                }
            }
        }
        }

    public void stopHologram() {
        spell[castingIndex].deleteHologram();
        castingIndex = -1;
        isCasting = false;
    }
    
}

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Components
    public Rigidbody rb;
    public Camera cam;
    public CameraController cameraController;

    //movement
    private float tempMovementSpeed;
    public float movementSpeed;
    public float rotationSpeed;

    //jumping
    public float jumpForce;
    private bool jumpRequest;
    private bool isGrounded;
    public static float normalGrav = Physics.gravity.y;
    public static float fallMultiplier = normalGrav * 2.5f;
    public static float lowJumpMultiplier = normalGrav * 2f;

    //spells
    public Spells2[] spell;
    public KeyCode[] keys;
    private int castingIndex;
    private bool isCasting;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();
        tempMovementSpeed = movementSpeed;

    }

    void FixedUpdate() { 

        //Movement
        Vector3 xMov = (movementSpeed * transform.right * Time.deltaTime * Input.GetAxis("Horizontal"));
        Vector3 zMov = (movementSpeed * transform.forward * Time.deltaTime * Input.GetAxis("Vertical"));
        rb.MovePosition(transform.position + xMov + zMov);

        //Gravity 4 better jumping
        if(rb.velocity.y < 0) {
            Physics.gravity = new Vector3(0, fallMultiplier,0);
        }else if (rb.velocity.y >0 && !Input.GetKey(KeyCode.Space)) { 
            Physics.gravity = new Vector3(0, lowJumpMultiplier, 0);
        }else {
            Physics.gravity = new Vector3(0, normalGrav, 0);
        }


        //Jumping
        if (jumpRequest) {
            rb.AddForce((Vector3.up * Time.deltaTime * (jumpForce * 100)), ForceMode.Impulse);
            jumpRequest = false;
        }

    }

    void Update() {
        if (!GameController.settingsOpen) {

            if (Input.GetKeyDown(KeyCode.Space)) {
                if (isGrounded) {
                    jumpRequest = true;
                }
            }


            //Change movement when ball is focused
            if (cameraController.isBallFocused) {
                transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed, 0, Space.Self);
            } 

            //Free Look if alt pressed
            if (!Input.GetKey(KeyCode.LeftAlt) && !cameraController.isBallFocused) {
                transform.eulerAngles = new Vector3(0, cam.transform.eulerAngles.y);
            }

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


    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Ground") {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision) {
        if (collision.gameObject.tag == "Ground") {
            isGrounded = false;
        }
    }

    public void stopHologram() {
        spell[castingIndex].deleteHologram();
        castingIndex = -1;
        isCasting = false;
    }
    
}

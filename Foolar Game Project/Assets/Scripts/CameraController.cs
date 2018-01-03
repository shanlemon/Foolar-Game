using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject crosshair;
	private bool crosshairActive = true;
    public bool lockCursor = true;
    public static float mouseSensitivity;
    public Transform target;
    public float distFromTarget = 2;
    public Vector2 pitchMinMax = new Vector2(-52, 85);

    public float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;
    public Transform player;
    Transform ball;

    public bool isBallFocused = false;
    public float yaw, pitch, tempYaw, tempPitch;


    void Start() {
        mouseSensitivity = 5f; 
        if (lockCursor) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
		ball = GameObject.Find("Ball").transform;

	}


	void LateUpdate() {

        if (!GameController.settingsOpen) {
            yaw += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
        }
            if (Input.GetKeyDown(KeyCode.LeftAlt)) {
                tempYaw = yaw;
                tempPitch = pitch;

            } else if (Input.GetKeyUp(KeyCode.LeftAlt)) {
                yaw = yaw - ((yaw % 360) - (tempYaw % 360));
                pitch = tempPitch;
            }

            if (Input.GetKeyDown(KeyCode.B)) {
                isBallFocused = !isBallFocused;
				crosshair.SetActive(!crosshairActive);
            }

            if (isBallFocused) {
                //crosshair.SetActive(false);
                Quaternion lookRotation = Quaternion.LookRotation(ball.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, .9f);
            } else {
                //crosshair.SetActive(true);
                currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
                transform.eulerAngles = currentRotation;

            }

            transform.position = target.position - transform.forward * distFromTarget;

            Vector3 pos = transform.position;
            pos.y = Mathf.Clamp(transform.position.y, 1, 100);
            transform.position = pos;

            Quaternion rot = transform.localRotation;
            rot.x = Mathf.Clamp(rot.x, -.43f, .64f);
            transform.localRotation = rot;
         
    }
}   


    

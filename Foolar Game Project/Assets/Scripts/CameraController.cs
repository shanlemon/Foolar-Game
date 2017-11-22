using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject crosshair;
    public bool lockCursor = true;
    public float mouseSensitivity = 5;
    public Transform target;
    public float distFromTarget = 2;
    public Vector2 pitchMinMax = new Vector2(-10, 85);

    public float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;
    public Transform player;
    public Transform ball;

    public bool isBallFocused = false;
    public float yaw, pitch, tempYaw, tempPitch;


    void Start() {
        if (lockCursor) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void LateUpdate() {
        yaw += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        if (Input.GetKeyDown(KeyCode.LeftAlt)) {
            tempYaw = yaw;
            tempPitch = pitch;

        } else if (Input.GetKeyUp(KeyCode.LeftAlt)) {
            yaw = yaw - ((yaw % 360) - (tempYaw % 360));
            pitch = tempPitch;
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            isBallFocused = !isBallFocused;
        }

        if (isBallFocused) {
            crosshair.SetActive(false);
            Quaternion lookRotation = Quaternion.LookRotation(ball.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSmoothTime * 6);
        } else {
            crosshair.SetActive(true);
            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
            transform.eulerAngles = currentRotation;

        }

        transform.position = target.position - transform.forward * distFromTarget;


    }


}

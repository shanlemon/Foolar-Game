using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public bool lockCursor = true;
    public float mouseSensitivity = 5;
    public Transform target;
    public float distFromTarget = 2;
    public Vector2 pitchMinMax = new Vector2(-10, 85);

    public float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;
    public Transform player;

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

        }else if (Input.GetKeyUp(KeyCode.LeftAlt)) {
            yaw = yaw - ((yaw % 360) - (tempYaw % 360));
            pitch = tempPitch;
        }

          
        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * distFromTarget;


    }


}

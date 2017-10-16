using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject target;
    public float smoothSpeed = 10f;
    private Vector3 offset;


    void Start() {
        offset = transform.position - target.transform.position;

    }

    void LateUpdate() {
        Vector3 desiredPosition = target.transform.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = desiredPosition;


    }


}

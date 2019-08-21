using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    Vector3 offset;

    void LateUpdate() {
        offset = new Vector3(0f, 1.6f, -1f);
        transform.position = target.position + offset; 
    }

}

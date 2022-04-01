using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconController : MonoBehaviour
{

    private Camera mainCamera;

    void Awake() {
        mainCamera = Camera.main;
    }

    // Follows the camera 
    void Update() {
        // Camera direction
        Vector3 dir = mainCamera.transform.position - transform.position;
        
        // Follows just the position
        //transform.rotation = (Quaternion.LookRotation(dir, Vector3.up)) * Quaternion.Euler(0, -90, 0);

        transform.rotation = mainCamera.transform.rotation * Quaternion.Euler(0, 90, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform parentTrans;
    [SerializeField] private float scale = 1;
    [SerializeField] private float minScale = 1;
    [SerializeField] private float maxScale = 10;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float scaleSpeed = 1f;
    // min and max angle from the up vector where 0 is strait down and 180 is strait up
    [SerializeField] private float minAngle = 10f;
    [SerializeField] private float maxAngle = 120f;


    private float distance;
    private Vector3 up;

    private float angleVert;
    private float angleHor;

    // Start is called before the first frame update
    void Start() {
        //center = new Vector3(0, 0, 0);
        up = new Vector3(0, 1, 0);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        angleVert = Vector3.Angle(transform.position - parentTrans.position, up);
    }

    // Update is called once per frame
    void Update() {
        // Rotate left and right
        transform.RotateAround(parentTrans.position, up, Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed);
        
        // zoom in and out clamping to max distance and any object between the camera player
        scale = Mathf.Clamp(scale + (Time.deltaTime * scaleSpeed * -Input.mouseScrollDelta.y), minScale, maxScale); 
        RaycastHit hit;
        if(Physics.Raycast(parentTrans.position, transform.position - parentTrans.position, out hit, scale)){
            transform.localPosition = transform.localPosition.normalized * hit.distance;
        } else {
            transform.localPosition = transform.localPosition.normalized * scale;
        }

        Vector3 back = transform.localPosition.normalized;
        back = new Vector3(back.x, 0, back.z);
        Vector3 right = -Vector3.Cross(back, up);        

        // Rotate up and down
        float deltaAngle = Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;
        if(deltaAngle + angleVert >= maxAngle) {
            transform.RotateAround(parentTrans.position, right, maxAngle - angleVert);
            angleVert = maxAngle;
        } else if(deltaAngle + angleVert <= minAngle) {
            transform.RotateAround(parentTrans.position, right, minAngle - angleVert);
            angleVert = minAngle;
        } else {
            transform.RotateAround(parentTrans.position, right, deltaAngle);
            angleVert += deltaAngle;
        }
    }
}

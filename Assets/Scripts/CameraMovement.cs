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
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;

    private float distance;
    private Vector3 center;
    private Vector3 up;
    // Start is called before the first frame update
    void Start() {
        center = new Vector3(0, 0, 0);
        up = new Vector3(0, 1, 0);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {
        
        transform.RotateAround(parentTrans.position, up, Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed);
        scale = Mathf.Clamp(scale + (Time.deltaTime * scaleSpeed * -Input.mouseScrollDelta.y), minScale, maxScale); 
        transform.localPosition = transform.localPosition.normalized * scale;
        
        Vector3 back = transform.localPosition.normalized;
        back = new Vector3(back.x, 0, back.z);
        Vector3 right = Vector3.Cross(back, up);
        transform.RotateAround(parentTrans.position, right, -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed);

    }
}

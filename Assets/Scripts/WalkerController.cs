using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerController : MonoBehaviour
{
    [SerializeField] private float speed = 1f; // speed of the whole walker
    [SerializeField] private float footAdjustSpeed = 1f; // speed at which the walker adjusts its feet

    [SerializeField] private GameObject targetFrontLeft;
    [SerializeField] private GameObject targetFrontRight;
    [SerializeField] private GameObject targetBackLeft;
    [SerializeField] private GameObject targetBackRight;
    
    // Default foot location
    public Vector3 centerFrontLeft;
    public Vector3 centerFrontRight;
    public Vector3 centerBackLeft;
    public Vector3 centerBackRight;

    // Determines whether to move with ground or pick up feet
    private bool isGroundedFrontLeft = true;
    private bool isGroundedFrontRight = true;
    private bool isGroundedBackLeft = true;
    private bool isGroundedBackRight = true;

    // Current angle used during foot pickup cycle 
    private float angleFrontLeft;
    private float angleFrontRight;
    private float angleBackLeft;
    private float angleBackRight;

    private float maxRadius = 4.5f; // farthest foot can move from relative origin

    // default forward direction
    private Vector3 forward = new Vector3(0, 0, -1);
    private Vector3 right;

    // Start sets default of each foot positions, assumes targets are in center position
    void Start(){
        right = Vector3.Cross(new Vector3(0, 1, 0), forward).normalized;
        centerFrontLeft = targetFrontLeft.transform.position;
        centerFrontRight = targetFrontRight.transform.position;
        centerBackLeft = targetBackLeft.transform.position;
        centerBackRight = targetBackRight.transform.position;
        targetFrontLeft.transform.position += forward * Random.Range(-maxRadius / 2, -maxRadius);
        targetFrontRight.transform.position += forward * Random.Range(maxRadius / 2, maxRadius);
        targetBackLeft.transform.position += forward * Random.Range(maxRadius / 2, maxRadius);
        targetBackRight.transform.position += forward * Random.Range(-maxRadius / 2, -maxRadius);
    }

    // Does the calculations for where the foot will go
    private void UpdateFoot(Transform targetTrans, Vector3 footCenter, ref bool isGrounded, ref float angle) {
        if(isGrounded) {
            targetTrans.localPosition -= forward * Time.deltaTime * speed;
            if((footCenter - targetTrans.position).magnitude >= maxRadius){
                isGrounded = false;
                angle = 0f;
            }
        } else {
            // Determines the foot pick up speed
            float deltaAngle;
            deltaAngle =  footAdjustSpeed * Time.deltaTime * (Mathf.Pow(angle/90f - 1, 2) + .5f);
            /* 
            if(angle < 90) {
                deltaAngle = footAdjustSpeed * Time.deltaTime * .5f; 
            } else {
                deltaAngle = footAdjustSpeed * Time.deltaTime * 2f; 
            }
            */
            targetTrans.RotateAround(footCenter, right, deltaAngle);
            angle += deltaAngle;
            if(angle >= 180f) {
                targetTrans.position = footCenter + (forward * maxRadius);
                isGrounded = true;
            }
        }
    }

    void FixedUpdate() {
        transform.position += forward * Time.deltaTime * speed;
        
        centerFrontLeft += forward * Time.deltaTime * speed;
        centerFrontRight += forward * Time.deltaTime * speed;
        centerBackLeft += forward * Time.deltaTime * speed;
        centerBackRight += forward * Time.deltaTime * speed;
        UpdateFoot(targetFrontLeft.transform, centerFrontLeft, ref isGroundedFrontLeft, ref angleFrontLeft);
        UpdateFoot(targetFrontRight.transform, centerFrontRight, ref isGroundedFrontRight, ref angleFrontRight);
        UpdateFoot(targetBackLeft.transform, centerBackLeft, ref isGroundedBackLeft, ref angleBackLeft);
        UpdateFoot(targetBackRight.transform, centerBackRight, ref isGroundedBackRight, ref angleBackRight);
    }
}

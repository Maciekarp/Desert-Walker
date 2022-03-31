using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform camTrans;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpStrength = 5f;
    [SerializeField] private float climbSpeed = 10f;
    
    private Rigidbody playerRB;
    private Vector3 upVect;
    
    private string state = "falling";

    private bool canJump = true;
    [SerializeField] private bool canInfiniteJump = true;

    private GameObject surface = null;
    private Vector3 surfacePrevPos;

    // Start is called before the first frame update
    void Start() {
        playerRB = GetComponent<Rigidbody>();
        upVect = new Vector3(0, 1, 0);
        canJump = true;
    }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject != surface) {
            surface = other.gameObject;
            //transform.SetParent(surface.transform);
            surfacePrevPos = surface.transform.position;
        }
    }

    void OnCollisionStay(Collision other) {
        canJump = true;
        state = "onGround";
        //playerRB.useGravity = false;
        // state = "climbing";
    }

    void OnCollisionExit(Collision other) {
        if(other.gameObject == surface) {
            surface = null;
            //transform.SetParent(null);
            canJump = canInfiniteJump;
            state = "falling";
        }
        //playerRB.useGravity = true;

    }

    // Update is called once per frame
    void Update() {
        //transform.localScale = defaultScale;

        // using the camera determines forward and right direction
        Vector3 forwardVect = Vector3.Normalize(this.transform.position - camTrans.position);
        forwardVect = new Vector3(forwardVect.x, 0, forwardVect.z);
        Vector3 rightVect = Vector3.Cross(upVect, forwardVect).normalized;
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if(state == "onGround") {
            if(Input.GetKeyDown("space") && canJump){
                playerRB.velocity += new Vector3(0, jumpStrength, 0);
                canJump = canInfiniteJump;
            }
            playerRB.velocity = 
                (forwardVect * (vertical * speed * Time.deltaTime)) + 
                (rightVect * (horizontal * speed * Time.deltaTime)) +
                new Vector3(0, playerRB.velocity.y, 0);
        } else if(state == "climbing") {
            playerRB.velocity += new Vector3(0, climbSpeed * Time.deltaTime, 0);
        } else {
            playerRB.velocity = 
                (forwardVect * (vertical * speed * Time.deltaTime)) + 
                (rightVect * (horizontal * speed * Time.deltaTime)) +
                new Vector3(0, playerRB.velocity.y, 0);
        }
    }
    
    
    // follows movement of surface
    void LateUpdate() {
        if(surface != null) {
            transform.position += (surface.transform.position - surfacePrevPos);
            surfacePrevPos = surface.transform.position;
        }
    }
    
}

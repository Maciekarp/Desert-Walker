using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform camTrans;

    [SerializeField] private SurfUnder surfUnder;

    [SerializeField] private Animator animator;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpStrength = 5f;
    [SerializeField] private float climbSpeed = 10f;
    [SerializeField] private float jumpDelay = 0.1f;


    private Rigidbody playerRB;
    private Vector3 upVect;
    
    // Change in movement caused by surface attached to 
    private Vector3 deltaSurf;

    private string state = "falling";

    //private bool canJump = true;
    private float lastJump;

    private GameObject surface = null;
    private Vector3 surfacePrevPos;

    // Start is called before the first frame update
    void Start() {
        playerRB = GetComponent<Rigidbody>();
        upVect = new Vector3(0, 1, 0);
        //canJump = true;
        deltaSurf = Vector3.zero;
        lastJump = Time.time;
    }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject != surface) {
            surface = other.gameObject;
            //transform.SetParent(surface.transform);
            surfacePrevPos = surface.transform.position;
        }
    }

    // Returns the amount moved by surface attached to
    public Vector3 DeltaSurf() {
        return deltaSurf;
    }

    void Update() {
        // Follows movement of surface
        if(surface != null) {
            deltaSurf = (surface.transform.position - surfacePrevPos);
            transform.position += deltaSurf;
            surfacePrevPos = surface.transform.position;
        } else {
            deltaSurf = Vector3.zero;
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        if(surfUnder.OnGround()) {
            state = "onGround";
        } else {
            state = "falling";
        }
        //transform.localScale = defaultScale;

        // using the camera determines forward and right direction
        Vector3 forwardVect = Vector3.Normalize(this.transform.position - camTrans.position);
        forwardVect = new Vector3(forwardVect.x, 0, forwardVect.z);
        Vector3 rightVect = Vector3.Cross(upVect, forwardVect).normalized;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        // if on ground
        if(state == "onGround") {
            if(Input.GetKey("space") && Time.time - lastJump > jumpDelay){
                playerRB.velocity += new Vector3(0, jumpStrength, 0);
                lastJump = Time.time;
            }
            playerRB.velocity = 
                (forwardVect * (vertical * speed * Time.deltaTime)) + 
                (rightVect * (horizontal * speed * Time.deltaTime)) +
                new Vector3(0, playerRB.velocity.y, 0);
            if(vertical != 0 || horizontal != 0) {
                animator.SetBool("isWalking", true);
            } else {
                animator.SetBool("isWalking", false);
            } 
        // if climbing 
        } else if(state == "climbing") {
            playerRB.velocity += new Vector3(0, climbSpeed * Time.deltaTime, 0);
        } else {
            playerRB.velocity = 
                (forwardVect * (vertical * speed * Time.deltaTime)) + 
                (rightVect * (horizontal * speed * Time.deltaTime)) +
                new Vector3(0, playerRB.velocity.y, 0);
        }
    }
}

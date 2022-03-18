using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform camTrans;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpStrength = 5f;
    
    private Rigidbody playerRB;
    private Vector3 upVect;

    // Start is called before the first frame update
    void Start() {
        playerRB = GetComponent<Rigidbody>();
        upVect = new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void Update() {
        // using the camera determines forward and right direction
        Vector3 forwardVect = Vector3.Normalize(this.transform.position - camTrans.position);
        forwardVect = new Vector3(forwardVect.x, 0, forwardVect.z);
        Vector3 rightVect = Vector3.Cross(upVect, forwardVect).normalized;
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        if(Input.GetKeyDown("space")){
            playerRB.velocity += new Vector3(0, jumpStrength, 0);
        }
        playerRB.velocity += 
            (forwardVect * (vertical * speed * Time.deltaTime)) + 
            (rightVect * (horizontal * speed * Time.deltaTime));
        //playerRB.velocity += new Vector3(horizontal * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconController : MonoBehaviour {

    private Camera mainCamera;

    // Used to keep movement with object it is attached to
    private Vector3 prevPos = Vector3.zero;
    [HideInInspector] public GameObject attachedObject = null;

    // Variables used for animating icon
    [SerializeField] private float scaleTime = 1; // Time it takes to complete animation
    [SerializeField] [Range (1, 2)] private float maxScale = 1.1f; // This is actually the sqrt of the max scale
    private Vector3 targetScale;
    private bool startingUp = true;
    private float startTime;
    private float endApear;
    
    private bool disapearing = false;

    // Removes the icon by starting the disapear animation
    public void KillIcon() {
        startingUp = false;
        disapearing = true;
        //Destroy(this.gameObject);

        startTime = Time.time;
    }

    void Start() {
        mainCamera = Camera.main; // Initializes Camera to look at


        targetScale = transform.localScale;
        transform.localScale = 0.0001f * targetScale;
        endApear= maxScale + Mathf.Sqrt(maxScale * maxScale - 1); // function end
        startTime = Time.time;
    }

    // Follows the camera 
    void Update() {
        // When the icon is starting up do the "Apear" animation
        if(startingUp) {
            float curr = ((Time.time - startTime) / scaleTime) * endApear;
            transform.localScale = ((-curr * curr) + 2 * maxScale * curr) * targetScale;
            if(curr >= endApear) {
                startingUp = false;
                transform.localScale = targetScale;
            }
        }

        if(disapearing) {
            float curr = (1 - ((Time.time - startTime) / scaleTime)) * endApear;
            transform.localScale = ((-curr * curr) + 2 * maxScale * curr) * targetScale;
            if(curr <= 0) {
                Destroy(this.gameObject);
            }
        }

        // follow object the icon is attached to if it exists
        if(attachedObject != null) {
            if(prevPos == Vector3.zero){
                prevPos = attachedObject.transform.position;
            } else {
                transform.position += attachedObject.transform.position - prevPos;
                prevPos = attachedObject.transform.position;
            }
        }

        // Face camera at all times
        Vector3 dir = mainCamera.transform.position - transform.position;
        transform.rotation = mainCamera.transform.rotation * Quaternion.Euler(0, 90, 0);
    }
}

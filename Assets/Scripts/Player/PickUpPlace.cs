using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPlace : MonoBehaviour {
    // Layers that should be tracked as interactable
    [SerializeField] private LayerMask mask;
    
    [SerializeField] private GameObject iconPrefab;
    private IconController currIcon;
    [SerializeField] private float iconHeight = 0.5f;

    [SerializeField] private Transform pickDropLocation; 
    [SerializeField] private float timeToShowIcon;
    private float timeFocused = 0;
    // list of objects in players pick up area
    private List<GameObject> canPickUp;
    private GameObject currFocus = null;

    // Start is called before the first frame update
    void Start() {
        canPickUp = new List<GameObject>();
    }

    // Used to calculate The new closest Object
    private void SetClosest() {
        if(canPickUp.Count == 0) {
            currFocus = null;
            ResetIcon();
        }
        GameObject closest = null;
        foreach(GameObject curr in canPickUp) {
            if(closest == null) {
                closest = curr;
            }
            if((pickDropLocation.position - closest.transform.position).magnitude > (pickDropLocation.position - curr.transform.position).magnitude) {
                closest = curr;
            }
        }
        // If a new closer object is found reset the icon
        if(currFocus != closest) {
            ResetIcon();
        }
        currFocus = closest;
    }

    // resets the icon and timer
    private void ResetIcon() {
        if(currIcon != null) {
            currIcon.KillIcon();
            currIcon = null;
        }
        timeFocused = 0;
    }

    // When an object get in the spot allows it to be picked up
    void OnTriggerEnter(Collider other) {
        if ((mask.value & (1 << other.transform.gameObject.layer)) > 0) {
            canPickUp.Add(other.gameObject);
            SetClosest();
        }
    }
    
    // When an object leaves the spot remove from list
    void OnTriggerExit(Collider other) {
        if ((mask.value & (1 << other.transform.gameObject.layer)) > 0) {
            RemoveItem(other.gameObject);
        }
    }

    // used to interact with an object will return empty if no objects exist in area
    public GameObject GetInteract() {
        return currFocus;
    }

    // removes item from list and updates to a new closest item
    public void RemoveItem(GameObject obj) {
        canPickUp.Remove(obj);
        // if the icon is attached to the item getting removed reset curr icon and timer
        if(currIcon != null && obj == currIcon.attachedObject) {
            ResetIcon();
        }
        SetClosest();
    }

    void Update() {

        // checks if a new item is the nearest one
        if(canPickUp.Count > 1) {
            SetClosest();
        }
        // count up time focusing on target
        if(currFocus != null) {
            timeFocused += Time.deltaTime;
        }

        // if enough time has passed spawn the icon above the item
        if(timeFocused > timeToShowIcon && currIcon == null) {
            currIcon = Instantiate(
                iconPrefab, 
                currFocus.transform.position + new Vector3(0, iconHeight, 0),
                Quaternion.identity
            ).GetComponent<IconController>();
            currIcon.attachedObject = currFocus;
        }
    }
}

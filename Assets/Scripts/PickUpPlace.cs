using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPlace : MonoBehaviour {
    // list of objects in players pick up area
    private List<GameObject> canPickUp;

    // Start is called before the first frame update
    void Start() {
        canPickUp = new List<GameObject>();
    }

    // When an object get in the spot allows it to be picked up
    void OnTriggerEnter(Collider other) {
        canPickUp.Add(other.gameObject);
    }
    
    void OnTriggerExit(Collider other) {
        canPickUp.Remove(other.gameObject);
    }

    // used to interact with an object will return empty if no objects exist in area
    public GameObject GetInteract() {
        if(canPickUp.Count != 0) {
            return canPickUp[0];
        }
        return null;
    }
}

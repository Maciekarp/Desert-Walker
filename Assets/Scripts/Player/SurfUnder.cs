using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfUnder : MonoBehaviour
{
    // Layers that should be tracked as surfaces that can be stood on
    [SerializeField] private LayerMask mask;

    // list of objects in standing area
    private List<GameObject> underSurfs;

    public bool OnGround() {
        print(underSurfs.Count);
        return underSurfs.Count > 0;
    }

    // Start is called before the first frame update
    void Start() {
        underSurfs = new List<GameObject>();
    }

    // When an object get in the spot allows it to be picked up
    void OnTriggerEnter(Collider other) {
        if ((mask.value & (1 << other.transform.gameObject.layer)) > 0) {
            print("Added Object");
            underSurfs.Add(other.gameObject);
        }
    }
    
    // When an object leaves the spot remove from list
    void OnTriggerExit(Collider other) {
        if ((mask.value & (1 << other.transform.gameObject.layer)) > 0) {
            print("Removed Object");
            underSurfs.Remove(other.gameObject);
        }
    }
}

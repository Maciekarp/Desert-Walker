// When item spawns makes it fall and stick to first surface it hits

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAndStick : MonoBehaviour {
    [SerializeField] private LayerMask attachLayers;
    [SerializeField] private float fallSpeed = 1;
    [SerializeField] private float maxDist = 1;
    //[SerializeField] private Rigidbody rb;

    private Transform attachedSurf;
    private Vector3 prevSurfPos;

    void Start() {
        GetComponent<Collider>().isTrigger = false;
    }

    /*
    void OnCollisionStay(Collision other) {
        if ((mask.value & (1 << other.transform.gameObject.layer)) > 0) {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            this.GetComponent<FallAndStick>().enabled = false;
            transform.SetParent(other.gameObject.transform, true);
        }
    }
    */

    // Update is called once per frame
    void FixedUpdate() {
        if(attachedSurf == null) {
            RaycastHit hit;
            Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, attachLayers);
            if((transform.position - hit.point).magnitude < maxDist) {
                transform.position = hit.point;
                attachedSurf = hit.collider.gameObject.transform;
                prevSurfPos = attachedSurf.position;
            } else {
                transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            }
        } else {
            transform.position += attachedSurf.position - prevSurfPos;
            prevSurfPos = attachedSurf.position;
        }
    }
}

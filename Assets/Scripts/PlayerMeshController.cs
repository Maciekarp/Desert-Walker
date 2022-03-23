using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeshController : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 1f;

    private Vector3 prevPos;

    // Start is called before the first frame update
    void Start() {
        prevPos = transform.position;
    }

    void LateUpdate(){
        if((prevPos - transform.position).magnitude > 0.01f) {
            Vector3 dir = transform.position - prevPos;
            dir = new Vector3(dir.x, 0, dir.z);
            if(dir == Vector3.zero){
                return;
            }
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.LookRotation(dir, Vector3.up),
                Time.deltaTime * turnSpeed
            );
            prevPos = transform.position;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeshController : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 1f;
    [SerializeField] private PlayerMovement movementCnt;

    private Vector3 prevPos;

    // Start is called before the first frame update
    void Start() {
        prevPos = transform.position;
    }

    // Rotates the player mesh depending on its movement
    void LateUpdate() {
        Vector3 dir = transform.position - (prevPos + (movementCnt.DeltaSurf()));
        dir = new Vector3(dir.x, 0, dir.z);
        if(dir == Vector3.zero){
            prevPos = transform.position;
            return;
        }
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.LookRotation(dir, Vector3.up),// * Quaternion.Euler(-90, 0, 0),
            Time.deltaTime * turnSpeed
        );
        prevPos = transform.position;
    }
}

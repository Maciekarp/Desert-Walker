using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoSphere : MonoBehaviour {
    [SerializeField] private bool showGizmo = true;
    [SerializeField] [Range(0, 1)] private float size = 1f;
    [SerializeField] private Color color = Color.red;

    void OnDrawGizmos() {
        if(showGizmo) {
            Gizmos.color = color;
            Gizmos.DrawSphere(transform.position, size);
        }
    }
}
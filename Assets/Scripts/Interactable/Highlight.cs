using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField] private GameObject highlight;
    [SerializeField] private LayerMask mask;

    // Start is called before the first frame update
    void Start() {
        highlight.SetActive(false);    
    }

    void OnTriggerEnter(Collider other) {
        if ((mask.value & (1 << other.transform.gameObject.layer)) > 0) {
            highlight.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other) {
        if ((mask.value & (1 << other.transform.gameObject.layer)) > 0) {
            highlight.SetActive(false);
        }
    }
}

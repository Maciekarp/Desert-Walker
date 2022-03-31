using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField] private GameObject highlight;

    // Start is called before the first frame update
    void Start() {
        highlight.SetActive(false);    
    }

    void OnTriggerEnter(Collider other) {
        highlight.SetActive(true);
    }

    void OnTriggerExit(Collider other) {
        highlight.SetActive(false);
    }
}

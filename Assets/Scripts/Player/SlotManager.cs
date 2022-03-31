using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    [SerializeField] private GameObject stickMesh;
    [SerializeField] private GameObject stickHighlight;
    //[SerializeField] private GameObject rock;
    //[SerializeField] private GameObject stick;
    
    [SerializeField] private string currState = "";

    // Sets the slot to the default state
    void Start() {
        SetSlot(currState);
        ClearHighlight();
    }

    // method used to visually update the slot and set state 
    public void SetSlot(string type) {
        if(type == "") {
            currState = "";
            ClearHighlight();
            stickMesh.SetActive(false);
        }
        if(type == "stick") {
            currState = "stick";
            stickMesh.SetActive(true);
        }
    }

    public void Highlight() {
        if(currState == "stick") {
            stickHighlight.SetActive(true);
        }
    }

    public void ClearHighlight() {
        stickHighlight.SetActive(false);
    }

    // getter function returning current state of slot
    public string GetState() {
        return currState;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryIcon : MonoBehaviour
{
    [SerializeField] private GameObject stick;
    [SerializeField] private SlotManager slot;

    private void SetIcon(string type) {
        if(type == "stick") {
            stick.SetActive(true);
        }
        if(type == "") {
            stick.SetActive(false);
        }
    }

    void Update() {
        SetIcon(slot.GetState());        
    }
}

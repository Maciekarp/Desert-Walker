using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private SlotManager[] backPackSlots;
    private int currHighlighted = -1;
    // Update is called once per frame

    void Update() {
        // if one of the slots is pressed gets the index for that one
        int pressedSlot = -1;
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            pressedSlot = 0;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            pressedSlot = 1;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)) {
            pressedSlot = 2;
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)) {
            pressedSlot = 3;
        }

        // Sets the highlight
        if(pressedSlot != -1) {
            ClearAllHighlighted();
            if(currHighlighted != pressedSlot){
                backPackSlots[pressedSlot].Highlight();
            } else {
                currHighlighted = -1;
            }
        }
    }

    private void ClearAllHighlighted(){
        foreach(SlotManager slot in backPackSlots) {
            slot.ClearHighlight();
        }
    }
}

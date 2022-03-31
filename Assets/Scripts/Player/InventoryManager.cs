using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    [SerializeField] private ItemList itemList;
    [SerializeField] private SlotManager[] backPackSlots;
    [SerializeField] private PickUpPlace pickPlace;
    [SerializeField] private Transform dropLocation;
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
                currHighlighted = pressedSlot;
            } else {
                currHighlighted = -1;
            }
        }

        // Pick up item / Interact / Use
        // relies on puckupplace script to find the correct object if multiple exist
        if(Input.GetKeyDown(KeyCode.F)) {
            GameObject obj = pickPlace.GetInteract();
            if(obj == null) {
                // do nothing since there is nothing to interact with
            } else {
                // do stuff specific to the object being interacted with
                InteractInfo info = obj.GetComponent(typeof(InteractInfo)) as InteractInfo;
                if(info == null) {
                    Debug.LogAssertion("object: " + obj + "\nhas no InteractInfo component!");
                } else {
                    if(info.GetObjectType() == "stick") {
                        // Try to pick up stick
                    }
                }
            }
        }

        // Drop item
        if(Input.GetKeyDown(KeyCode.Q)) {
            if(currHighlighted != -1) {
                // drop item if it is highlighted and exists
                string itemType = backPackSlots[currHighlighted].GetState();
                backPackSlots[currHighlighted].SetSlot("");

                if(itemType != "") {
                    GameObject prefab = itemList.GetItemPrefab(itemType);
                    Instantiate(prefab, dropLocation.position, dropLocation.rotation);
                }
            }
        }
    }

    private void ClearAllHighlighted(){
        foreach(SlotManager slot in backPackSlots) {
            slot.ClearHighlight();
        }
    }
}

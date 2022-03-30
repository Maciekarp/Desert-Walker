using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public struct Item {
    public string name;
    public GameObject prefab;
}

public class ItemList  : MonoBehaviour {
    [SerializeField] private Item[] itemList;

    // tries to find item specified by name reutrns null if item does not exist
    public GameObject GetItemPrefab(string type) {
        foreach(Item item in itemList) {
            if(item.name == type) {
                return item.prefab;
            }
        }
        return null;
    }
}

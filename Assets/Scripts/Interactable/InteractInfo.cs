using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractInfo : MonoBehaviour {
    [SerializeField] private string objectType;

    public string GetObjectType(){
        return objectType;
    }
}

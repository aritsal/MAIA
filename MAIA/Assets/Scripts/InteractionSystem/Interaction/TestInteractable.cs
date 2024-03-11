using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class TestInteractable : MonoBehaviour 
{
    private void Start() {
        this.GetComponent<Interactable>().OnInteractKeyUp += (_) => Debug.Log("Interact"); 
    }
}

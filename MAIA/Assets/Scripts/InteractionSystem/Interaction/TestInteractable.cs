using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class TestInteractable : MonoBehaviour 
{
    private void Start() {
        this.GetComponent<Interactable>().onInteractKeyUp += (_) => Debug.Log("Interact"); 
    }
}

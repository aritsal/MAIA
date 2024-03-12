using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessInitializer : MonoBehaviour
{   
    public List<ItemProcessor> itemProcessors = new List<ItemProcessor>();

    private void Awake() {
        Debug.Log("Initialized item processor dictionaries; if your item processes are only producing default items, you likely don't have it included here.");
        this.itemProcessors.ForEach((p) => p.InitializeDictionary());
    }
}   

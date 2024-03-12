using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.EditorTools;
using UnityEngine;

/// <summary>
/// A class used to describe the pairs of objects t
/// </summary>
[CreateAssetMenu(fileName = "New Item Processor", menuName = "Scriptable Objects/Item Processor", order = 0)]
public class ItemProcessor : ScriptableObject {
    [SerializeField] private List<ItemProcess> _processList = new List<ItemProcess>();
    
    /// <summary>
    /// The input is ignored, it only serves as a fallback for undefined processes
    /// </summary>
    [field: SerializeField] public ItemProcess defaultProcess { get; private set; } = new ItemProcess();

    public Dictionary<Item, ItemProcess> processes { get; private set; }= new Dictionary<Item, ItemProcess>();
    
    public void InitializeDictionary() {
        this.processes ??= new Dictionary<Item, ItemProcess>();
        this.processes.Clear();
        this.processes.TrimExcess();
        this._processList.ForEach((process) => {
            if (this.processes.TryAdd(process.input, process)) return;
            else {
                Debug.LogError($"Duplicate input in item process list ({process.input} -> {process.output}, {process.duration}s)");
                return;
            }
        });
    }
}


[Serializable]
public class ItemProcess {
    [field: SerializeField] public Item input { get; private set; }
    [field: SerializeField] public Item output { get; private set; }
    /// <summary> The time it takes for this item to process. </summary>
    [field: SerializeField] public float duration { get; private set; }
}
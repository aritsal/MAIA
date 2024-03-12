using System.Collections;
using UnityEngine;

[RequireComponent(typeof(StaticSlot))]
public class FoodProcessor : MonoBehaviour
{
    private StaticSlot _slot;
    [SerializeField] private ItemProcessor _itemProcessor;
    public float progress { get; private set; }

    private void Start() {
        this._slot = this.GetComponent<StaticSlot>();

        this._slot.onPlace += () => {
            this._currentProcess = this.StartCoroutine(this.ProcessItem_Coroutine(this._slot.currentItem));
        };

        this._slot.onRemove += () => {
            if (this._currentProcess != null)
                this.StopCoroutine(this._currentProcess);
        };
    }


    private Coroutine _currentProcess;
    private IEnumerator ProcessItem_Coroutine(ItemRef itemRef) {
        if (!this._itemProcessor.processes.TryGetValue(itemRef.item, out ItemProcess process))
            process = this._itemProcessor.defaultProcess;

        float timeElapsed = 0f;
        while (timeElapsed < process.duration) {
            yield return null;
            // Just pause if disabled
            if (this.enabled)
                timeElapsed += Time.deltaTime;
            this.progress = timeElapsed / process.duration;
        }

        this.progress = 0f;
        this._currentProcess = null;

        ItemRef output = process.output.Get().GetComponent<ItemRef>();
        // Equip item handles orphaning the old item
        this._slot.EquipItem(output);
        
        // Release the initial input item
        if (itemRef.TryGetComponent(out PooledObject po)) po.Release();
        else GameObject.Destroy(itemRef.gameObject);    
    }
}

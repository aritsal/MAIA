using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HandSlot : MonoBehaviour
{
    public ItemRef currentItem { get; private set; }

    public bool isEmpty => currentItem == null;

    /// <summary>
    /// Destroys the current item, if holding one
    /// </summary>
    public void DestroyItem() {
        if (this.currentItem == null) return;

        this.currentItem.transform.SetParent(null, true);
        this.currentItem.OnDrop?.Invoke();

        if (this.currentItem.TryGetComponent(out PooledObject pooledObject)) 
            pooledObject.Release();
        else 
            Destroy(this.currentItem.gameObject);
            
        this.currentItem = null;
    }

    /// <summary>
    /// Drops the current item, if holding one
    /// </summary>
    public void DropItem() {
        if (this.currentItem == null) return;

        this.currentItem.transform.SetParent(null, true);
        this.currentItem.OnDrop?.Invoke();
        this.currentItem = null;
    }

    /// <summary>
    /// Drops the current item, then equips the input item
    /// </summary>
    public void EquipItem(ItemRef item) {
        if (this.currentItem != null)
            this.DropItem();

        this.currentItem = item;
        item.transform.SetParent(this.transform, false);
        item.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        item.OnHold?.Invoke();
    }
}

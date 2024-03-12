using UnityEngine;

public class HandSlot : MonoBehaviour
{
    public ItemRef currentItem { get; private set; }

    public bool isEmpty => this.currentItem == null;

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
    /// Drops the current item, if holding one, and returns it
    /// </summary>
    public ItemRef DropItem() {
        if (this.currentItem == null) return null;

        this.currentItem.transform.SetParent(null, true);
        this.currentItem.OnDrop?.Invoke();
        ItemRef item = this.currentItem;
        this.currentItem = null;
        return item;
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

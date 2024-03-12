using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HandSlot : MonoBehaviour
{
    public ItemRef currentItem { get; private set; }

    public bool isEmpty => this.currentItem == null;

    // If the child is moved/destroyed, it'll let go of the reference
    private void OnTransformChildrenChanged() {
        if (this.currentItem == null) return;
        if (this.currentItem.transform.parent != this.transform)
            this.DropItem();
    }

    private void Start() {
        ItemRef itemRef = this.GetComponentInChildren<ItemRef>();

        if (itemRef == null) return;
        this.EquipItem(itemRef);
    }

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
        if (this.currentItem == null) {
            this.currentItem = null;
            return null;
        }

        this.currentItem.OnDrop?.Invoke();
        ItemRef item = this.currentItem;
        this.currentItem.transform.SetParent(null, true);
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

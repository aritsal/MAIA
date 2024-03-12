using System;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class StaticSlot : MonoBehaviour
{
    public ItemRef currentItem { get; private set; }
    public Action onPlace;
    public Action onRemove;

    private void Start() {
        if (this.transform.childCount == 0) return;
        ItemRef item = this.GetComponentInChildren<ItemRef>();
        if (item != null) 
            this.EquipItem(item);
    }

    private void OnEnable() {
        this.GetComponent<Interactable>().onInteractKeyUp += this.TryPlaceItem;
    }

    private void OnDisable() {
        this.GetComponent<Interactable>().onInteractKeyUp -= this.TryPlaceItem;
    }

    public void ForceDropItem() {
        this.onRemove?.Invoke();
        this.currentItem.OnHold -= this.TransferToHand;
        this.currentItem.transform.SetParent(null, true);
        this.currentItem = null;
    }

    public void TryPlaceItem(InteractionHandler handler) {
        // No item, don't care
        if (handler.handSlot.isEmpty) return;

        ItemRef item = handler.handSlot.DropItem();

        if (this.currentItem == null) 
            this.EquipItem(item);
        
    }

    public void EquipItem(ItemRef itemRef) {
        if (this.currentItem != null) 
            this.ForceDropItem();

        // We only want the rb to be disabled, we will allow players to pick it up
        if (itemRef.TryGetComponent(out Rigidbody rb))
            rb.isKinematic = true;
        if (itemRef.TryGetComponent(out Collider collider))
            collider.enabled = true;

        itemRef.transform.SetParent(this.transform, false);
        itemRef.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        this.currentItem = itemRef;

        // Drop the item when the player holds it
        this.currentItem.OnHold += this.TransferToHand;
        this.onPlace?.Invoke();
    }

    private void TransferToHand() {
        this.onRemove?.Invoke();
        this.currentItem.OnHold -= this.TransferToHand;
        // Setting the item's parent is already handled by the hand slot
        this.currentItem = null;
    }
}

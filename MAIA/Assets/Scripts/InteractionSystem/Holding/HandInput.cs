using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInput : MonoBehaviour
{
    [SerializeField] private HandSlot _handSlot;
    [SerializeField] private HoverableDetector _hoverableDetector;
    [SerializeField] private float throwVelocity = 10f;
    [SerializeField] private float additionalUpwardsThrowVelocity = 5f;

    private void Update() {
        if (this._handSlot.isEmpty) {
            this.TryGrab();
        }
        else {
            this.TryThrow();
            this.TryDrop();
        }
    }

    private void TryGrab() {
        if (!Input.GetMouseButtonUp(0)) return;
        if (!this._hoverableDetector.isHovering) return;
        if (!this._hoverableDetector.hoverable.TryGetComponent(out ItemRef itemRef)) return;
        this._handSlot.EquipItem(itemRef);
    }

    private void TryDrop() {
        if (!Input.GetMouseButtonUp((int)LegacyMouseButton.Left)) return;
        this._handSlot.DropItem();
    }

    private void TryThrow() {
        if (!Input.GetMouseButton((int)LegacyMouseButton.Right)) return;
        ItemRef itemRef = this._handSlot.DropItem();

        if (!itemRef.TryGetComponent(out Rigidbody rb)) return;
        rb.AddForce(this.transform.forward * this.throwVelocity + Vector3.up * this.additionalUpwardsThrowVelocity, ForceMode.VelocityChange);
        rb.AddForceAtPosition(itemRef.transform.forward, itemRef.transform.position + itemRef.transform.up, ForceMode.VelocityChange);
    }
}

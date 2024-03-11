using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInput : MonoBehaviour
{
    [SerializeField] private HandSlot _handSlot;
    [SerializeField] private HoverableDetector _hoverableDetector;

    private void Update() {
        if (this._handSlot.isEmpty) this.TryGrab();
        else this.TryDrop();
    }

    private void TryGrab() {
        if (!Input.GetMouseButtonUp(0)) return;
        if (!this._hoverableDetector.isHovering) return;
        if (!this._hoverableDetector.hoverable.TryGetComponent(out ItemRef itemRef)) return;
        this._handSlot.EquipItem(itemRef);
    }

    private void TryDrop() {
        if (!Input.GetMouseButtonUp(0)) return;
        this._handSlot.DropItem();
    }
}

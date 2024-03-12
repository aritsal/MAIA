using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [field: SerializeField] public HandSlot handSlot { get; private set; }
    [SerializeField] private HoverableDetector _hoverableDetector;
    [field: SerializeField] public KeyCode interactKey { get; private set; }

    private void Update() {
        this.TryInteractDown();
        this.TryInteractUp();
    }

    private void TryInteractUp() {
        if (!Input.GetKeyUp(this.interactKey)) return;
        if (!this._hoverableDetector.isHovering) return;
        if (!this._hoverableDetector.hoverable.TryGetComponent(out Interactable interactable)) return;

        interactable.onInteractKeyUp?.Invoke(this);
    }

    private void TryInteractDown() {
        if (!Input.GetKeyDown(this.interactKey)) return;
        if (!this._hoverableDetector.isHovering) return;
        if (!this._hoverableDetector.hoverable.TryGetComponent(out Interactable interactable)) return;

        interactable.onInteractKeyUp?.Invoke(this);
    }
}

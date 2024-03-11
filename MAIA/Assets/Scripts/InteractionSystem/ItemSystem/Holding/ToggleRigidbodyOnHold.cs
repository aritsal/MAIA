using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

[RequireComponent(typeof(ItemRef), typeof(Rigidbody))]
public class ToggleRigidbodyOnHold : MonoBehaviour
{
    private ItemRef _itemRef;
    private Rigidbody _rigidbody;
    private Collider _collider;

    private void Awake() {
        this._itemRef = this.GetComponent<ItemRef>();
        this._rigidbody = this.GetComponent<Rigidbody>();
        this._collider = this.GetComponent<Collider>();
    }

    private void OnEnable() {
        this._itemRef.OnHold.AddListener(this.OnHold);
        this._itemRef.OnDrop.AddListener(this.OnDrop);
    }

    private void OnDisable() {
        this._itemRef.OnHold.RemoveListener(this.OnHold);
        this._itemRef.OnDrop.RemoveListener(this.OnDrop);
    }

    private void OnHold() {
        _rigidbody.isKinematic = true;
        _collider.enabled = false;
    }

    private void OnDrop() {
        _rigidbody.isKinematic = false;
        _collider.enabled = true;
    }
}

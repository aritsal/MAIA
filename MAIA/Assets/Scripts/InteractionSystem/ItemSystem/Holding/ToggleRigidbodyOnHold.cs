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
        this._itemRef.OnHold += this.OnHold;
        this._itemRef.OnDrop += this.OnDrop;
    }

    private void OnDisable() {
        this._itemRef.OnHold -= this.OnHold;
        this._itemRef.OnDrop -= this.OnDrop;
    }

    private void OnHold() {
        this._rigidbody.isKinematic = true;
        this._collider.enabled = false;
    }

    private void OnDrop() {
        this._rigidbody.isKinematic = false;
        this._collider.enabled = true;
    }
}

using System;
using UnityEngine;

public class Hoverable : MonoBehaviour
{
    public const string HOVERABLE_LAYER_NAME = "Hoverable";

    public bool isBeingHovered => this.currentHoverableDetector != null;
    public HoverableDetector currentHoverableDetector;
    public Action<HoverableDetector> onHoverEnter;
    public Action<HoverableDetector> onHoverExit;
    private void Awake() {
        #if UNITY_EDITOR
        if (LayerMask.LayerToName(this.gameObject.layer) != HOVERABLE_LAYER_NAME) {
            Debug.LogWarning($"This object, {this.gameObject.name}, should be on layer {HOVERABLE_LAYER_NAME}. It's fixed automatically, but is should be manually set for preformance and clarity in prefabs.");
            this.gameObject.layer = LayerMask.NameToLayer(HOVERABLE_LAYER_NAME);     
        }
        #endif

        this.onHoverEnter += (h) => this.currentHoverableDetector = h;
        this.onHoverExit += (h) => this.currentHoverableDetector = h;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemRef : MonoBehaviour, IHoverable
{
    [field: SerializeReference] public Item item;
    public UnityEvent OnHold;
    public UnityEvent OnDrop;

    public void OnHoverEnter(HoverableDetector hoverableDetector) {}

    public void OnHoverExit(HoverableDetector hoverableDetector) {}
}

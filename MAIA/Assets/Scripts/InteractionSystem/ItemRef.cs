using System;
using UnityEngine;

[RequireComponent(typeof (Hoverable))]
public class ItemRef : MonoBehaviour
{
    [field: SerializeReference] public Item item;
    public Action OnHold;
    public Action OnDrop;
}
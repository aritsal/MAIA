using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof (Hoverable))]
public class ItemRef : MonoBehaviour
{
    [field: SerializeReference] public Item item;
    public Action OnHold;
    public Action OnDrop;
}

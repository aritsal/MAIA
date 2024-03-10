using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemRef : MonoBehaviour
{
    [field: SerializeReference] public Item item;
    public UnityAction OnHold;
    public UnityAction OnDrop;
}

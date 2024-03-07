using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicHoverable : MonoBehaviour, IHoverable
{
    public void OnHoverEnter()
    {
        Debug.Log("HOVERING " + this.name);
    }

    public void OnHoverExit()
    {
        Debug.Log("NO LONGER HOVERING " + this.name);
    }
}

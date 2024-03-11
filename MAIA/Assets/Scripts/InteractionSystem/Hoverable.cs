using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoverable : MonoBehaviour
{
    public Action<HoverableDetector> OnHoverEnter;
    public Action<HoverableDetector> OnHoverExit;
}

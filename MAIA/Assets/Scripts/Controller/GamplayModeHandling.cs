using System;
using UnityEngine;
[DefaultExecutionOrder(1000)]
public class GameplayModeHandling : MonoBehaviour
{
    public static Action onEnterFirstPersonMode;
    public static Action onExitFirstPersonMode;

    public static Action onEnterUiMode;
    public static Action onExitUiMode;

    private void Start() {
        onEnterFirstPersonMode?.Invoke();
    }
}

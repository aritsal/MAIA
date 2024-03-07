using System;
using UnityEngine;
[DefaultExecutionOrder(1000)]
public class GameplayModeHandling : MonoBehaviour
{
    public static Action OnEnterFirstPersonMode;
    public static Action OnExitFirstPersonMode;

    public static Action OnEnterUiMode;
    public static Action OnExitUiMode;

    private void Start() {
        OnEnterFirstPersonMode?.Invoke();
    }
}

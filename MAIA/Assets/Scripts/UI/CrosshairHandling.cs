using UnityEngine;
using UnityEngine.UI;

public class CrosshairHandling : MonoBehaviour
{
    private Image _image;
    [SerializeField] private Texture2D _crosshair;
    [SerializeField] private Texture2D _crosshairHoverInteractable;

    private void Start()
    {
        _image = GetComponent<Image>();   
    }

    private void OnEnable() {
        GameplayModeHandling.OnEnterFirstPersonMode += OnEnterFirstPersonMode;
        GameplayModeHandling.OnExitFirstPersonMode += OnExitFirstPersonMode;
    }

    private void OnDisable() {
        GameplayModeHandling.OnEnterFirstPersonMode -= OnEnterFirstPersonMode;
        GameplayModeHandling.OnExitFirstPersonMode -= OnExitFirstPersonMode;
    }



    private void OnEnterFirstPersonMode() {
        _image.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnExitFirstPersonMode() {
        _image.enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

}

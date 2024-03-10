using UnityEngine;
using UnityEngine.UI;

public class CrosshairHandling : MonoBehaviour
{
    private Image _image;
    [SerializeField] private Texture2D _crosshair;
    [SerializeField] private Texture2D _crosshairHoverInteractable;

    private void Start()
    {
        this._image = this.GetComponent<Image>();   
    }

    private void OnEnable() {
        GameplayModeHandling.onEnterFirstPersonMode += this.OnEnterFirstPersonMode;
        GameplayModeHandling.onExitFirstPersonMode += this.OnExitFirstPersonMode;
    }

    private void OnDisable() {
        GameplayModeHandling.onEnterFirstPersonMode -= this.OnEnterFirstPersonMode;
        GameplayModeHandling.onExitFirstPersonMode -= this.OnExitFirstPersonMode;
    }



    private void OnEnterFirstPersonMode() {
        this._image.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnExitFirstPersonMode() {
        this._image.enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

}

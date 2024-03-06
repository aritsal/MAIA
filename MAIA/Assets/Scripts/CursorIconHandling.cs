using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CursorIconHandling : MonoBehaviour
{
    public CursorIcon Crosshair;
    public CursorIcon CrosshairInteractable;

    public CursorIcon Pointer;
    public CursorIcon HoverInteractable;
    public CursorIcon ClickInteractable;

    private void Start() {
        FirstPerson();
    }

    private void FirstPerson() {
        SetCursor(Crosshair);
    }

    private void SetCursor(CursorIcon cursorIcon) {
        Cursor.SetCursor(cursorIcon.Texture, cursorIcon.Texture.Size() * cursorIcon.HotspotUV, CursorMode.Auto);
    } 

    [Serializable]
    public struct CursorIcon {
        public Vector2 HotspotUV;
        public Texture2D Texture;
    } 

    public enum GameState {
        InUI,
        InFirstPerson
    }
}

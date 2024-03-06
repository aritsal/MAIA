using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInput : MonoBehaviour
{
    public Vector2 RotationSpeed = Vector2.one;
    [Min(0)]public float Range = 90f;
    private Vector2 _camRotation;

    private Vector2 MouseInput() {
        return new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")).normalized;
    }

    private void Update() {
        _camRotation += MouseInput() * RotationSpeed;
        float x = _camRotation.x;
        float y = _camRotation.y;
        
        x %= 360f;

        y %= 360f;

        x = Mathf.Clamp(x, -Range, Range);
        
        _camRotation.x = x;
        _camRotation.y = y;

        transform.eulerAngles = _camRotation;
    }
}

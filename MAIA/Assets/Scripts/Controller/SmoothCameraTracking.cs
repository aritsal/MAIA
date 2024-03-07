using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraTracking : MonoBehaviour
{
    public Transform Target;
    public float PositionSmoothingTime = 0.05f;
    public float RotationSmoothingTime = 0.4f; 


    private void LateUpdate() {
        DampPosition();
        DampRotation();
    }

    private Vector3 _posVelocity;

    private void DampPosition() {
        // Smooth damp the position to target
        transform.position = Vector3.SmoothDamp(transform.position, Target.position, ref _posVelocity, PositionSmoothingTime, float.MaxValue, Time.deltaTime);
    }


    private float _yRotVelocity;
    private float _xRotVelocity;
    private void DampRotation() {
        float y = Mathf.SmoothDampAngle(transform.eulerAngles.y, Target.eulerAngles.y, ref _yRotVelocity, RotationSmoothingTime, float.MaxValue, Time.deltaTime);
        float x = Mathf.SmoothDampAngle(transform.eulerAngles.x, Target.eulerAngles.x, ref _xRotVelocity, RotationSmoothingTime, float.MaxValue, Time.deltaTime);
        transform.eulerAngles = new Vector3(x, y);
    }
}

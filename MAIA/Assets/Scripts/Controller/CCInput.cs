using System;
using UnityEngine;
using UnityEngine.Animations;

// "CC" stands for "CharacterController"
[RequireComponent(typeof(RotationConstraint)), RequireComponent(typeof(CharacterController))]
public class CCInput : MonoBehaviour
{
    [Header("The rotation constraint is automatically retrieved :)")]
    private CharacterController _characterController;
    public float DecelerationSpeed = 5f;
    public float AccelerationSpeed = 5f;
    public float MaxVelocity = 10f;
    public float GravityAcceleration = -25f; // jupiter gravity lmao

    private void Start()
    {
        var s = new ConstraintSource();
        s.sourceTransform = Camera.main.transform;
        s.weight = 1f;
        GetComponent<RotationConstraint>().AddSource(s);

        _characterController = GetComponent<CharacterController>();
        // This prevents the controller from accelerating if too high
        _characterController.minMoveDistance = 0f;
    }

    private void Update() {
        Debug.DrawRay(transform.position, transform.TransformDirection(InputVector() * MaxVelocity), Color.magenta);
        Debug.DrawRay(transform.position, Celerate(_characterController.velocity, transform.TransformDirection(InputVector())), Color.green);

        _characterController.Move(
            Gravity(
            Celerate(_characterController.velocity, transform.TransformDirection(InputVector()))) 
            * Time.deltaTime);
    }

    private Vector3 Gravity(Vector3 velocity) {
        if (_characterController.isGrounded)
            return new Vector3(velocity.x, 0f, velocity.z);
        else 
            return velocity + Vector3.up * GravityAcceleration * Time.deltaTime;
    }

    private Vector3 Celerate(Vector3 velocity, Vector3 input) {
        if (input.x == 0f && input.z == 0f) 
            return Decelerate();
        else    
            return Accelerate();

        Vector3 Decelerate() {
            Vector3 xz = new Vector3(velocity.x, 0f, velocity.z);
            xz = Vector3.MoveTowards(xz, Vector3.zero, DecelerationSpeed * Time.deltaTime);
            // Preserve the y velocity
            return new Vector3(xz.x, velocity.y, xz.z);
        }

        Vector3 Accelerate() {
            Vector3 xz = new Vector3(velocity.x, 0f, velocity.z);
            xz = Vector3.MoveTowards(xz, input * MaxVelocity, AccelerationSpeed * Time.deltaTime);
            // Preserve the y velocity
            return new Vector3(xz.x, velocity.y, xz.z);
        }
    }

    /// <summary>
    /// Returns WASD input as a normalized vector on the XZ plane
    /// </summary>
    private Vector3 InputVector() {
        return new Vector3(
            (Input.GetKey(KeyCode.D) ? 1f : 0f) + (Input.GetKey(KeyCode.A) ? -1f : 0f),
            0f,
            (Input.GetKey(KeyCode.W) ? 1f : 0f) + (Input.GetKey(KeyCode.S) ? -1f : 0f)
        ).normalized;
    }
}

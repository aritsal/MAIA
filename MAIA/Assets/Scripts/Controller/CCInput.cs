using System;
using UnityEngine;
using UnityEngine.Animations;

// "CC" stands for "CharacterController"
[RequireComponent(typeof(RotationConstraint)), RequireComponent(typeof(CharacterController))]
public class CCInput : MonoBehaviour
{
    [Header("The rotation constraint is automatically retrieved :)")]
    private CharacterController _characterController;
    public float decelerationSpeed = 5f;
    public float accelerationSpeed = 5f;
    public float maxVelocity = 10f;
    public float gravityAcceleration = -25f; // jupiter gravity lmao

    private void Start()
    {
        var s = new ConstraintSource();
        s.sourceTransform = Camera.main.transform;
        s.weight = 1f;
        this.GetComponent<RotationConstraint>().AddSource(s);

        this._characterController = this.GetComponent<CharacterController>();
        // This prevents the controller from accelerating if too high
        this._characterController.minMoveDistance = 0f;
    }

    private void Update() {
        Debug.DrawRay(this.transform.position, this.transform.TransformDirection(this.InputVector() * this.maxVelocity), Color.magenta);
        Debug.DrawRay(this.transform.position, this.Celerate(this._characterController.velocity, this.transform.TransformDirection(this.InputVector())), Color.green);

        this._characterController.Move(
            this.Gravity(
            this.Celerate(this._characterController.velocity, this.transform.TransformDirection(this.InputVector()))) 
            * Time.deltaTime);
    }

    private Vector3 Gravity(Vector3 velocity) {
        if (this._characterController.isGrounded)
            return new Vector3(velocity.x, 0f, velocity.z);
        else 
            return velocity + Vector3.up * this.gravityAcceleration * Time.deltaTime;
    }

    private Vector3 Celerate(Vector3 velocity, Vector3 input) {
        if (input.x == 0f && input.z == 0f) 
            return Decelerate();
        else    
            return Accelerate();

        Vector3 Decelerate() {
            Vector3 xz = new Vector3(velocity.x, 0f, velocity.z);
            xz = Vector3.MoveTowards(xz, Vector3.zero, this.decelerationSpeed * Time.deltaTime);
            // Preserve the y velocity
            return new Vector3(xz.x, velocity.y, xz.z);
        }

        Vector3 Accelerate() {
            Vector3 xz = new Vector3(velocity.x, 0f, velocity.z);
            xz = Vector3.MoveTowards(xz, input * this.maxVelocity, this.accelerationSpeed * Time.deltaTime);
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

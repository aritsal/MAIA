using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(RotationConstraint))]
public class ConstrainRotationToMainCamera : MonoBehaviour
{
    private void Start() {
        // Lazy var names .. change .. sometime later
        ConstraintSource s = new ConstraintSource {
            weight = 1f,
            sourceTransform = Camera.main.transform
        };
        RotationConstraint c = this.GetComponent<RotationConstraint>();
        c.AddSource(s);
        c.constraintActive = true;
    }
}

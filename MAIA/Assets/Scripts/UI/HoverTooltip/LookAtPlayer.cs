using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(LookAtConstraint))]
public class LookAtPlayer : MonoBehaviour
{
    private void Start() {
        Transform target = Camera.main.transform;
        LookAtConstraint lookAtConstraint = this.GetComponent<LookAtConstraint>();
        lookAtConstraint.AddSource(new ConstraintSource() {
            sourceTransform = target,
            weight = 1f
        });
    }
}

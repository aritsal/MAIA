using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(RotationConstraint))]
public class HoverableDetector : MonoBehaviour
{
    [field: Header("RotationConstraint is automatically handled by this script :)")]
    [field: SerializeField] private LayerMask _hoverableLayers;
    [field: SerializeField] private float _maxHoverableDistance = 5f;
    public IHoverable hoverable { get; private set; }
    public bool isHovering => this.hoverable != null;

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

    private void Update()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward);
        bool hasHit = Physics.Raycast(
            ray: new Ray(this.transform.position, this.transform.forward),
            maxDistance: this._maxHoverableDistance,
            layerMask: this._hoverableLayers,
            queryTriggerInteraction: QueryTriggerInteraction.Ignore,
            hitInfo: out RaycastHit hitInfo);
        
        // Exit hoverables when hovering over nothing 
        if (!hasHit) {
            this.hoverable?.OnHoverExit(this);
            this.hoverable = null;
            return;
        }

        // Remember the hoverable if it has a hoverable component
        if (hitInfo.collider.gameObject.TryGetComponent(out IHoverable newHoverable)) { 
            if (this.hoverable == newHoverable) return;

            this.hoverable?.OnHoverExit(this);
            newHoverable.OnHoverEnter(this);
            this.hoverable = newHoverable;
            return;
        }

        // Exit the last hoverable if hovering non hoverable
        this.hoverable?.OnHoverExit(this);
        this.hoverable = null;
    }
}

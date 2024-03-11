using UnityEngine;

public class HoverableDetector : MonoBehaviour
{
    [field: SerializeField] private LayerMask _hoverableLayers;
    [field: SerializeField] private float _maxHoverableDistance = 5f;
    public Hoverable hoverable { get; private set; }
    public bool isHovering => this.hoverable != null;

    private void Update() {
        this.CheckForHoverables();
    }

    private void CheckForHoverables() {
        Debug.DrawRay(this.transform.position, this.transform.forward);
        bool hasHit = Physics.Raycast(
            ray: new Ray(this.transform.position, this.transform.forward),
            maxDistance: this._maxHoverableDistance,
            layerMask: this._hoverableLayers,
            queryTriggerInteraction: QueryTriggerInteraction.Ignore,
            hitInfo: out RaycastHit hitInfo);

        // No hit, no hoverable 
        if (!hasHit) 
            this.SetHoverable(null); 

        // Only use the hoverable if it has the Hoverable component
        else if (hitInfo.collider.TryGetComponent(out Hoverable newHoverable)) 
            this.SetHoverable(newHoverable);
        
        // No component, no hoverable
        else
            this.SetHoverable(null);
    }

    private void SetHoverable(Hoverable newHoverable)  {
        // Do nothing if reference is the same
        if (this.hoverable == newHoverable) return;

        // Only call enter / exit actions if non-null
        if (this.hoverable != null) 
            this.hoverable.OnHoverExit?.Invoke(this);
        if (newHoverable != null)
            newHoverable.OnHoverEnter?.Invoke(this);
        
        this.hoverable = newHoverable;
    }
}

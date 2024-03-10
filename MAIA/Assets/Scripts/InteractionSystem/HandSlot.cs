using UnityEngine;

[RequireComponent(typeof (HoverableDetector))]
public class HandSlot : MonoBehaviour
{
    public ItemRef possession { get; private set; }
    private HoverableDetector _hoverableDetector;
    [SerializeField] private Transform _handTransform;

    private void Awake() {
        this._hoverableDetector = this.GetComponent<HoverableDetector>();
    }

    private void Update() {
        
    }

    /// <summary>
    /// Spawns an item in the players hand, and drops any item the player might have been holding
    /// </summary>
    public void Hold(Item item) {
        if (this.possession != null)
            this.Drop();

        GameObject itemObject = item.Get();
        this.possession = itemObject.GetComponent<ItemRef>();
        this.possession.transform.SetParent(this._handTransform, false);
        this.possession.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        // item.OnHold?.Invoke();
    }

    /// <summary>
    /// Drops the current possession
    /// </summary>
    public void Drop() {
        this.possession.OnDrop?.Invoke();
        this.possession.transform.SetParent(null, true);
        this.possession = null;
    }

    private bool TryGetItem(out ItemRef itemRef) {
        itemRef = null;

        // Return if not releasing left mouse button
        if (!Input.GetMouseButtonUp((int)LegacyMouseButton.Left))
            return false;

        // Return if not hovering anything
        if (this._hoverableDetector.hoverable == null)
            return false;

        // Return if the hovering item doesn't have an item ref
        if (!this._hoverableDetector.hoverable.gameObject.TryGetComponent(out ItemRef itemRef_))
            return false;
        
        itemRef = itemRef_;
        return true;
    }
}

using UnityEngine;

public class HandSlot : MonoBehaviour
{
    public ItemRef possession { get; private set; }
    

    public bool TryHold(ItemRef obj) {
        if (this.possession == null) {
            this.possession = obj;
            this.possession.transform.SetParent(this.transform, true);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Drops the current possession
    /// </summary>
    public void Drop() {
        this.possession.transform.SetParent(null, true);
    }
}

using System;
using UnityEngine.TerrainUtils;

public class Interactable : Hoverable
{
    public bool isBeingInteracted => currentInteractionHandler != null;
    public InteractionHandler currentInteractionHandler;
    public Action<InteractionHandler> OnInteractKeyUp;
    public Action<InteractionHandler> onInteractKeyDown;

    private void Awake() {
        this.onInteractKeyDown += (h) => this.currentInteractionHandler = h;
        this.OnInteractKeyUp += (h) => this.currentInteractionHandler = h;
    }
}

using System;
using UnityEngine.TerrainUtils;

public class Interactable : Hoverable
{
    public bool isBeingInteracted => currentInteractionHandler != null;
    public InteractionHandler currentInteractionHandler { get; private set; }
    public Action<InteractionHandler> onInteractKeyUp;
    public Action<InteractionHandler> onInteractKeyDown;

    private void Awake() {
        this.onInteractKeyDown += (h) => this.currentInteractionHandler = h;
        this.onInteractKeyUp += (h) => this.currentInteractionHandler = h;
    }
}

/// <summary>
/// To actually have this interface called, you need to have a collider on the interactable layer attached to the object with the interface
/// </summary>
public interface IHoverable {
    public void OnHoverEnter();
    public void OnHoverExit();
}

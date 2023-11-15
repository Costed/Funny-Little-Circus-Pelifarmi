using UnityEngine;

public interface IInteractable : IActivatable
{
    public abstract bool CanInteract();
    public abstract void Interact();
}

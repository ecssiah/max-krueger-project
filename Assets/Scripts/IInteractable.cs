using UnityEngine;

public interface IInteractable
{
    string Prompt { get; }
    
    bool CanInteract(Interactor interactor);
    void Interact(Interactor interactor);
}

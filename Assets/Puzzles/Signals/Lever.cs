using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Lever : PuzzleSignal, IInteractable
{
    [SerializeField] bool singleUse = false;

    Mover mover;

    void Awake()
    {
        mover = GetComponent<Mover>();
    }

    public void Interact(Interactor interactor)
    {
        if (singleUse && IsOn) 
        {
            return;
        }

        SetOn(!IsOn);

        if (IsOn)
        {
            mover.Activate();
        }
        else
        {
            mover.Deactivate();
        }
    }
}
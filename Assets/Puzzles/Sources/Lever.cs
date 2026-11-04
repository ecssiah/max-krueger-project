using UnityEngine;

public class Lever : PuzzleSignal, IInteractable
{
    [SerializeField] bool singleDirection;
    [SerializeField] Mover mover;

    void OnEnable() 
    {
        ChangedAction += ApplyVisual;
    }

    void OnDisable()
    {
        ChangedAction -= ApplyVisual;
    }

    public void Interact(Interactor interactor)
    {
        if (singleDirection && IsOn) 
        {
            return;
        }

        SetOn(!IsOn);
    }

    void ApplyVisual(PuzzleSignal _)
    {
        if (mover != null) 
        {
            mover.Set(IsOn);
        }
    }
}
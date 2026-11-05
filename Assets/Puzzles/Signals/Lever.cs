using UnityEngine;

public class Lever : PuzzleSignal, IInteractable
{
    [SerializeField] bool singleUse = false;

    [SerializeField] Mover mover;

    void OnEnable() 
    {
        SignalAction += ApplyVisual;
    }

    void OnDisable()
    {
        SignalAction -= ApplyVisual;
    }

    void ApplyVisual(PuzzleSignal _)
    {
        if (mover != null) 
        {
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

    public void Interact(Interactor interactor)
    {
        if (singleUse && IsOn) 
        {
            return;
        }

        SetOn(!IsOn);
    }
}
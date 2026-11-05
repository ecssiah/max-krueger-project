using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Door : PuzzleSignal
{
    [SerializeField] PuzzleSignal sourceSignal;

    Mover mover;

    void Awake()
    {
        mover = GetComponent<Mover>();
    }

    void OnEnable()
    {
        sourceSignal.SignalAction += HandleSignal;
    }

    void OnDisable()
    {
        sourceSignal.SignalAction -= HandleSignal;
    }

    void HandleSignal(PuzzleSignal _)
    {
        if (sourceSignal.IsOn)
        {
            mover.Activate();
        }
        else
        {
            mover.Deactivate();
        }
    }
}
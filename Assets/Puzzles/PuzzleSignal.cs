using System;
using UnityEngine;

public abstract class PuzzleSignal : MonoBehaviour
{
    public event Action<PuzzleSignal> SignalAction;

    public bool IsOn 
    { 
        get; 
        private set; 
    }

    public void SetOn(bool value)
    {
        if (IsOn == value) 
        {
            return;
        }

        IsOn = value;
        SignalAction?.Invoke(this);
    }
}
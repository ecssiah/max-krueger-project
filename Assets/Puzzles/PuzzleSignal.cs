using System;
using UnityEngine;

public abstract class PuzzleSignal : MonoBehaviour
{
    public event Action<PuzzleSignal> ChangedAction;

    public bool IsOn 
    { 
        get; 
        private set; 
    }

    protected void SetOn(bool value)
    {
        if (IsOn == value) 
        {
            return;
        }

        IsOn = value;
        ChangedAction?.Invoke(this);
    }

    public virtual void ResetSignal()
    {
        SetOn(false);  
    } 
}
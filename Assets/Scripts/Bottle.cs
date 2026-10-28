using System;
using UnityEngine;

public enum BottleType
{
    Green,
    Blue,
    Red,
    Cyan,
}

public class Bottle : MonoBehaviour, IInteractable
{
    [SerializeField] BottleType BottleType = BottleType.Green;

    public event Action<Bottle> ActivateAction;

    public bool IsActive { get; private set; }

    public string Prompt => "Press E";

    public bool CanInteract(Interactor interactor)
    {
        return !IsActive;
    }

    public void Interact(Interactor interactor)
    {
        IsActive = true;

        ActivateAction?.Invoke(this);

        print($"{BottleType} is {(IsActive ? "activated" : "deactivated")}");
    }

    public void Reset()
    {
        IsActive = false;
    }
}

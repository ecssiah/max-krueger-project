using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryCheck : PuzzleSignal, IInteractable
{
    [SerializeField] List<ItemDefinition> requiredItemList;
    [SerializeField] bool consumeItems;
    [SerializeField] UnityEvent onMissingItems;

    public void Interact(Interactor interactor)
    {
        if (IsOn) 
        {
            return;
        }

        if (!requiredItemList.TrueForAll(interactor.Inventory.Has))
        {
            onMissingItems.Invoke();
            return;
        }

        if (consumeItems)
        {
            foreach (ItemDefinition item in requiredItemList) 
            {
                interactor.Inventory.Remove(item);
            }
        }

        SetOn(true);
    }
}
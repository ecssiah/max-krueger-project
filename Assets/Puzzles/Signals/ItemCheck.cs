using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryCheck : PuzzleSignal, IInteractable
{
    [SerializeField] List<ItemDefinition> requiredItemDefinitionList;

    [SerializeField] bool consumeItems;
    
    [SerializeField] UnityEvent onMissingItems;

    public void Interact(Interactor interactor)
    {
        if (IsOn) 
        {
            return;
        }

        if (!requiredItemDefinitionList.TrueForAll(itemDefinition => interactor.Inventory.Has(itemDefinition)))
        {
            onMissingItems.Invoke();
            return;
        }

        if (consumeItems)
        {
            foreach (ItemDefinition itemDefinition in requiredItemDefinitionList) 
            {
                interactor.Inventory.Remove(itemDefinition);
            }
        }

        SetOn(true);
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemCheck : PuzzleSignal, IInteractable
{
    [SerializeField] List<ItemDefinition> requiredItemDefinitionList;

    [SerializeField] bool removesItems;

    [SerializeField] UnityEvent onMissingItems;

    public void Interact(Interactor interactor)
    {
        if (IsOn) 
        {
            return;
        }

        bool requirementMet = requiredItemDefinitionList.TrueForAll(
            itemDefinition => interactor.Inventory.Has(itemDefinition, 1)
        );

        if (requirementMet)
        {
            SetOn(true);

            if (removesItems)
            {
                foreach (ItemDefinition itemDefinition in requiredItemDefinitionList) 
                {
                    interactor.Inventory.Remove(itemDefinition, 1);
                }
            }
        }
        else
        {
            onMissingItems.Invoke();
        }
    }
}
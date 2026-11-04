using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    readonly List<ItemDefinition> itemDefinitionList = new();
    
    public event Action ChangedAction;

    public bool Has(ItemDefinition item_definition)
    {
        return itemDefinitionList.Contains(item_definition);
    }

    public void Add(ItemDefinition item_definition)    
    {
        itemDefinitionList.Add(item_definition); 
        
        ChangedAction?.Invoke(); 
    }

    public bool Remove(ItemDefinition item_definition) 
    { 
        bool removed = itemDefinitionList.Remove(item_definition); 
        
        if (removed) 
        {
            ChangedAction?.Invoke(); 
        }

        return removed;
    }
}
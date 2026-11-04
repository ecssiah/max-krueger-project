using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    readonly List<ItemDefinition> item_list = new();
    
    public event Action ChangedAction;

    public bool Has(ItemDefinition item_definition)
    {
        return item_list.Contains(item_definition);
    }

    public void Add(ItemDefinition item_definition)    
    { 
        item_list.Add(item_definition); 
        
        ChangedAction?.Invoke(); 
    }

    public bool Remove(ItemDefinition item_definition) 
    { 
        bool removed = item_list.Remove(item_definition); 
        
        if (removed) 
        {
            ChangedAction?.Invoke(); 
        }

        return removed;
    }
}
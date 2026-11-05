using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private readonly Dictionary<ItemDefinition, int> itemDefinitionDictionary = new Dictionary<ItemDefinition, int>();

    public event Action<ItemDefinition, int> ChangedAction;

    public IReadOnlyDictionary<ItemDefinition, int> GetItemDefinitionDictionary()
    {
        return itemDefinitionDictionary;
    }

    public int GetQuantity(ItemDefinition itemDefinition)
    {
        return itemDefinitionDictionary.GetValueOrDefault(itemDefinition, 0);
    }

    public bool Has(ItemDefinition itemDefinition, int amount)
    {
        return GetQuantity(itemDefinition) >= amount;
    }

    public void Add(ItemDefinition itemDefinition, int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        int newAmount = GetQuantity(itemDefinition) + amount;
        itemDefinitionDictionary[itemDefinition] = newAmount;
        
        ChangedAction?.Invoke(itemDefinition, newAmount); 
    }

    public bool Remove(ItemDefinition itemDefinition, int amount) 
    { 
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        if (!Has(itemDefinition, amount))
        {
            return false;
        }

        int newAmount = itemDefinitionDictionary[itemDefinition] - amount;
        
        if (newAmount == 0)
        {
            itemDefinitionDictionary.Remove(itemDefinition);
        }
        else
        {
            itemDefinitionDictionary[itemDefinition] = newAmount;
        }

        return true;
    }
}
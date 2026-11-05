using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatternCondition : PuzzleSignal
{
    [SerializeField] Dictionary<PuzzleSignal, bool> signalDictionary;

    void OnEnable()
    { 
        foreach (PuzzleSignal signal in signalDictionary.Keys)
        {
            signal.SignalAction += HandleSignal; 
        }
    }

    void OnDisable() 
    { 
        foreach (PuzzleSignal signal in signalDictionary.Keys)
        {
            signal.SignalAction -= HandleSignal; 
        }
    }

    void HandleSignal(PuzzleSignal _)
    {
        bool isOn = signalDictionary.All(entry => entry.Key.IsOn == entry.Value);

        SetOn(isOn);
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Condition : PuzzleSignal
{
    public enum Mode 
    { 
        All, 
        Any, 
        Sequence, 
        Count 
    }
    
    [SerializeField] Mode mode;
    [SerializeField] List<PuzzleSignal> signalList;

    [SerializeField] int requiredCount = 1;
    [SerializeField] bool resetOnFailure = true;

    int sequenceIndex;

    void OnEnable()
    { 
        foreach (PuzzleSignal signal in signalList)
        {
            signal.ChangedAction += OnInput; 
        }
    }

    void OnDisable() 
    { 
        foreach (PuzzleSignal signal in signalList)
        {
            signal.ChangedAction -= OnInput; 
        }
    }

    void OnInput(PuzzleSignal inputSignal)
    {
        if (IsOn) 
        {
            return;
        }

        bool result = mode switch
        {
            Mode.All      => signalList.TrueForAll(inputSignal => inputSignal.IsOn),
            Mode.Any      => signalList.Exists(inputSignal => inputSignal.IsOn),
            Mode.Count    => signalList.Count(inputSignal => inputSignal.IsOn) >= requiredCount,
            Mode.Sequence => EvaluateSequence(inputSignal),
            _ => false
        };

        print($"{gameObject.name} {result}");

        SetOn(result);
    }

    bool EvaluateSequence(PuzzleSignal inputSignal)
    {
        if (!inputSignal.IsOn) 
        {
            return sequenceIndex >= signalList.Count;
        }

        if (signalList[sequenceIndex] == inputSignal) 
        { 
            sequenceIndex++; 
            
            return sequenceIndex >= signalList.Count; 
        }
        
        if (resetOnFailure) 
        { 
            sequenceIndex = 0;

            foreach (PuzzleSignal input in signalList) 
            {
                input.ResetSignal(); 
            }
        }

        return false;
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllCondition : PuzzleSignal
{
    [SerializeField] List<PuzzleSignal> signalList;

    void OnEnable()
    { 
        foreach (PuzzleSignal signal in signalList)
        {
            signal.SignalAction += HandleSignal; 
        }
    }

    void OnDisable() 
    { 
        foreach (PuzzleSignal signal in signalList)
        {
            signal.SignalAction -= HandleSignal; 
        }
    }

    void HandleSignal(PuzzleSignal _)
    {
        bool allOn = signalList.All(signal => signal.IsOn);

        SetOn(allOn);

        if (allOn && name == "Solution")
        {
            PuzzleUI.Instance.SetText("Puzzle Complete!");
        }
    }
}
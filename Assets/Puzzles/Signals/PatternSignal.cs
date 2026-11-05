using System.Collections.Generic;
using UnityEngine;

public class PatternSignal : PuzzleSignal
{
    [SerializeField] List<PuzzleSignal> signalList;
    [SerializeField] List<int> requiredList;

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
        for (int i = 0; i < signalList.Count; i++)
        {
            if (requiredList.Contains(i))
            {
                if (!signalList[i].IsOn)
                {
                    SetOn(false);
                    return;
                }
            }
            else
            {
                if (signalList[i].IsOn)
                {
                    SetOn(false);
                    return;
                }
            }
        }

        SetOn(true);
    }
}
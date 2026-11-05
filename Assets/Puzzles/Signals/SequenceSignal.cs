using System.Collections.Generic;
using UnityEngine;

public class SequenceSignal : PuzzleSignal
{
    [SerializeField] List<PuzzleSignal> signalList;

    int sequenceIndex = 0;

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

    void HandleSignal(PuzzleSignal inputSignal)
    {
        if (inputSignal.IsOn && inputSignal == signalList[sequenceIndex])
        {
            sequenceIndex++;

            if (sequenceIndex == signalList.Count)
            {
                SetOn(true);
                return;
            }
        }
        else
        {
            sequenceIndex = 0;

            SetOn(false);

            foreach(PuzzleSignal signal in signalList)
            {
                signal.SetOn(false);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SequencePuzzle : MonoBehaviour
{
    [SerializeField] List<Bottle> CorrectOrder;
    
    [SerializeField] UnityEvent OnSolved;
    [SerializeField] UnityEvent OnFailed;

    int progress = 0;
    bool solved = false;

    void OnEnable()  
    {
        foreach (Bottle bottle in CorrectOrder)
        {
            bottle.ActivateAction += HandleActivate;
        }
    }

    void OnDisable() 
    { 
        foreach (Bottle bottle in CorrectOrder)
        {
            bottle.ActivateAction -= HandleActivate;
        }
    }

    void HandleActivate(Bottle bottle)
    {
        if (solved) return;

        if (bottle == CorrectOrder[progress])
        {
            progress++;

            if (progress == CorrectOrder.Count)
            {
                solved = true;
                OnSolved.Invoke();
            }
        }
        else
        {
            OnFailed.Invoke();
            StartCoroutine(ResetAfterDelay(0.75f));
        }
    }

    IEnumerator ResetAfterDelay(float time)
    {
        print("Puzzle is resetting.");

        yield return new WaitForSeconds(time);

        progress = 0;
        
        foreach (Bottle bottle in CorrectOrder)
        {
            bottle.Reset();
        }
    }
}

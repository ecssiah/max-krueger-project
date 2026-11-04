using UnityEngine;
using UnityEngine.Events;

public class PuzzleReceiver : MonoBehaviour
{
    [SerializeField] PuzzleSignal sourceSignal;
    
    [SerializeField] bool invertsSignal;
    
    [SerializeField] UnityEvent onActivated;
    [SerializeField] UnityEvent onDeactivated;

    void OnEnable()  
    { 
        sourceSignal.SignalAction += HandleSignal;

        HandleSignal(sourceSignal); 
    }

    void OnDisable() 
    { 
        sourceSignal.SignalAction -= HandleSignal; 
    }

    void HandleSignal(PuzzleSignal signal)
    {
        if (signal.IsOn ^ invertsSignal) 
        {
            onActivated.Invoke(); 
        }
        else 
        {
            onDeactivated.Invoke();
        }
    }
}
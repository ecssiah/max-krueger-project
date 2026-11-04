using UnityEngine;
using UnityEngine.Events;

public class PuzzleReceiver : MonoBehaviour
{
    [SerializeField] PuzzleSignal source;
    [SerializeField] bool inverted;
    [SerializeField] UnityEvent onActivated;
    [SerializeField] UnityEvent onDeactivated;

    void OnEnable()  
    { 
        source.ChangedAction += Handle;

        Handle(source); 
    }

    void OnDisable() 
    { 
        source.ChangedAction -= Handle; 
    }

    void Handle(PuzzleSignal signal)
    {
        if (signal.IsOn ^ inverted) 
        {
            onActivated.Invoke(); 
        }
        else 
        {
            onDeactivated.Invoke();
        }
    }
}
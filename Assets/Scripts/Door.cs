using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Transform DoorPanel;
    [SerializeField] Vector3 OpenOffset = new Vector3(1.4f, 0, 0);
    [SerializeField] float OpenDuration = 8.0f;

    Vector3 closed_position;
    Coroutine movement_coroutine;

    public bool IsLocked { get; private set; }

    public string Prompt => "Press E";

    void Awake() 
    {
        IsLocked = true;

        closed_position = DoorPanel.localPosition;
    }

    void MoveTo(Vector3 target)
    {
        if (movement_coroutine != null) 
        {
            StopCoroutine(movement_coroutine);
        }
        
        movement_coroutine = StartCoroutine(Slide(target));
    }

    IEnumerator Slide(Vector3 target)
    {
        Vector3 start_position = DoorPanel.localPosition;

        for (float time = 0; time < 1f; time += Time.deltaTime / OpenDuration)
        {
            DoorPanel.localPosition = Vector3.Lerp(start_position, target, Mathf.SmoothStep(0, 1, time));

            yield return null;
        }

        DoorPanel.localPosition = target;
    }

    public void Unlock()
    {
        if (!IsLocked) 
        {
            return;
        }
        
        IsLocked = false;

        Open();
    }

    public void Open()
    {
        MoveTo(closed_position + OpenOffset);  
    }

    public void Close()
    {
        MoveTo(closed_position);
    }
}

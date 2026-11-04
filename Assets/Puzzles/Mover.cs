using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] Vector3 activatedOffset = new Vector3(0, 3, 0);
    [SerializeField] Vector3 activatedRotation;
    [SerializeField] float duration = 1.0f;
    [SerializeField] AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    Vector3 deactivatedPosition;
    Quaternion deactivatedRotation;

    float progress;
    float goal;

    void Awake()
    {
        deactivatedPosition = transform.localPosition;
        deactivatedRotation = transform.localRotation;
    }

    public void Activate()
    {
        goal = 1.0f;
    }

    public void Deactivate()
    {
        goal = 0.0f;
    } 

    public void Set(bool on)
    {
        goal = on ? 1.0f : 0.0f;
    }

    void Update()
    {
        if (Mathf.Approximately(progress, goal)) 
        {
            return;
        }

        progress = Mathf.MoveTowards(progress, goal, Time.deltaTime / duration);

        float t = curve.Evaluate(progress);
        
        transform.SetLocalPositionAndRotation(
            deactivatedPosition + activatedOffset * t, 
            deactivatedRotation * Quaternion.Euler(activatedRotation * t)
        );
    }
}
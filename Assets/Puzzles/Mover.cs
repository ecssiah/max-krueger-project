using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] Vector3 activatedOffset = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField] Vector3 activatedRotation = new Vector3(0.0f, 0.0f, 0.0f);

    [SerializeField] float duration = 1.0f;

    [SerializeField] AnimationCurve animationCurve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1.0f, 1.0f);

    Vector3 deactivatedPosition;
    Quaternion deactivatedRotation;

    float progress;
    float target;

    void Awake()
    {
        deactivatedPosition = transform.localPosition;
        deactivatedRotation = transform.localRotation;
    }

    public void Activate()
    {
        target = 1.0f;
    }

    public void Deactivate()
    {
        target = 0.0f;
    } 

    void Update()
    {
        if (Mathf.Approximately(progress, target)) 
        {
            return;
        }

        progress = Mathf.MoveTowards(progress, target, Time.deltaTime / duration);

        float t = animationCurve.Evaluate(progress);
        
        transform.SetLocalPositionAndRotation(
            deactivatedPosition + activatedOffset * t, 
            deactivatedRotation * Quaternion.Euler(activatedRotation * t)
        );
    }
}
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] Camera camera;

    [SerializeField] float range = 0.5f;

    float interaction_timer = 1.0f;
    float cooldown = 1.0f;

    [SerializeField] LayerMask interactable_mask;

    IInteractable current;

    void Update()
    {
        current = null;
        interaction_timer += Time.deltaTime;

        Vector3 origin = camera.transform.position;
        Vector3 direction = camera.transform.forward;
        
        bool hit_occurred = Physics.Raycast(
            origin + 1.0f * direction, 
            direction,
            out var hit, 
            range, 
            interactable_mask
        );

        if (hit_occurred)
        {
            IInteractable target = hit.collider.GetComponentInParent<IInteractable>();
            
            if (target != null && target.CanInteract(this))
            {
                current = target;
            }

            Debug.DrawLine(origin, hit.point, Color.green, 2.0f);
            Debug.DrawRay(hit.point, hit.normal * 0.3f, Color.blue, 2.0f);
        }
        else
        {
            Debug.DrawRay(origin, direction * range, Color.red, 2.0f);
        }
    }

    public void OnInteract()
    {
        if (current != null && interaction_timer > cooldown)
        {
            current.Interact(this);
            interaction_timer = 0.0f;
        }
    }
}

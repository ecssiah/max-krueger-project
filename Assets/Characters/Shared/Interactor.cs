using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] Camera camera;

    [SerializeField] float range = 1.0f;

    float interactTimer = 1.0f;
    readonly float interactCooldown = 1.0f;

    [SerializeField] LayerMask interactMask;

    IInteractable current;

    public Inventory Inventory;

    void Update()
    {
        current = null;
        interactTimer += Time.deltaTime;

        Vector3 origin = camera.transform.position;
        Vector3 direction = camera.transform.forward;
        
        bool hitOccurred = Physics.Raycast(
            origin + 1.0f * direction, 
            direction,
            out RaycastHit hit, 
            range, 
            interactMask
        );

        if (hitOccurred)
        {
            IInteractable target = hit.collider.GetComponentInParent<IInteractable>();
            
            if (target != null)
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
        if (current != null && interactTimer > interactCooldown)
        {
            current.Interact(this);
            interactTimer = 0.0f;
        }
    }
}

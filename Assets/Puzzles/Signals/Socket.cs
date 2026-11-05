using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ItemSocket : PuzzleSignal, IInteractable
{
    [SerializeField] ItemDefinition itemDefinition;

    [SerializeField] Vector3 SocketOffset = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField] Vector3 SocketRotation = new Vector3(0.0f, 0.0f, 0.0f);

    MeshFilter displayMeshFilter;
    MeshRenderer displayMeshRenderer;

    void Awake()
    {
        foreach (Transform childTransform in transform)
        {
            displayMeshFilter = childTransform.GetComponentInChildren<MeshFilter>();
            displayMeshRenderer = childTransform.GetComponentInChildren<MeshRenderer>();

            if (displayMeshFilter != null && displayMeshRenderer != null)
            {
                break;
            }
        }
    }

    public void Interact(Interactor interactor)
    {
        if (IsOn || !interactor.Inventory.Has(itemDefinition, 1)) 
        {
            return;
        }

        interactor.Inventory.Remove(itemDefinition, 1);

        displayMeshRenderer.transform.SetLocalPositionAndRotation(
            SocketOffset, 
            Quaternion.Euler(SocketRotation)
        );
        
        displayMeshFilter.sharedMesh = itemDefinition.Mesh;
        displayMeshRenderer.sharedMaterial = itemDefinition.Material;
        displayMeshRenderer.enabled = true;

        SetOn(true);
    }
}
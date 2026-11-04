using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ItemSocket : PuzzleSignal, IInteractable
{
    [SerializeField] ItemDefinition socketItemDefinition;
    [SerializeField] ItemDefinition displayItemDefinition;

    MeshFilter displayItemMeshFilter;
    MeshRenderer displayItemMeshRenderer;

    [SerializeField] Transform displayItemTransform;

    public Vector3 SocketOffset;
    public Vector3 SocketRotation;

    void OnValidate()
    {
        ApplyVisuals();
    }

    void Awake()
    {
        displayItemMeshFilter = displayItemTransform.GetComponent<MeshFilter>();
        displayItemMeshRenderer = displayItemTransform.GetComponent<MeshRenderer>();
        displayItemMeshRenderer.enabled = false;

        ApplyVisuals();
    }

    void ApplyVisuals()
    {
        if (socketItemDefinition == null) 
        {
            return;
        }

        GetComponent<MeshFilter>().sharedMesh = socketItemDefinition.Mesh;
        GetComponent<MeshRenderer>().sharedMaterial = socketItemDefinition.Material;
    }

    public void Interact(Interactor interactor)
    {
        if (IsOn || !interactor.Inventory.Has(displayItemDefinition)) 
        {
            return;
        }

        interactor.Inventory.Remove(displayItemDefinition);

        displayItemTransform.SetLocalPositionAndRotation(
            SocketOffset, 
            Quaternion.Euler(SocketRotation)
        );
        
        displayItemMeshFilter.sharedMesh = displayItemDefinition.Mesh;
        displayItemMeshRenderer.sharedMaterial = displayItemDefinition.Material;
        displayItemMeshRenderer.enabled = true;
        
        SetOn(true);
    }
}
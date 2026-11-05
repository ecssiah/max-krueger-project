using UnityEngine;

public class Pickup : PuzzleSignal, IInteractable
{
    [SerializeField] ItemDefinition itemDefinition;

    void Awake()
    {
        UpdateMesh();
    }

    void OnValidate()
    {
        UpdateMesh();
    }

    void UpdateMesh()
    {
        if (itemDefinition == null) 
        {
            return;
        }

        MeshFilter meshFilter = GetComponentInChildren<MeshFilter>();
        MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();
        BoxCollider boxCollider = GetComponentInChildren<BoxCollider>();

        meshFilter.sharedMesh = itemDefinition.Mesh;
        meshRenderer.sharedMaterial = itemDefinition.Material;

        Bounds bounds = meshFilter.sharedMesh.bounds;

        boxCollider.center = bounds.center;
        boxCollider.size = bounds.size;

        meshFilter.transform.localScale = itemDefinition.MeshScale;
    }

    public void Interact(Interactor interactor)
    {
        interactor.Inventory.Add(itemDefinition, 1);

        gameObject.SetActive(false);
    }
}
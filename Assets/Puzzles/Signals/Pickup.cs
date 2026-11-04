using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Pickup : PuzzleSignal, IInteractable
{
    [SerializeField] ItemDefinition itemDefinition;

    void OnValidate()
    {
        ApplyVisuals();
    }

    void Awake()
    {
        ApplyVisuals();
    }

    void ApplyVisuals()
    {
        if (itemDefinition == null) 
        {
            return;
        }

        GetComponent<MeshFilter>().sharedMesh = itemDefinition.Mesh;
        GetComponent<MeshRenderer>().sharedMaterial = itemDefinition.Material;
    }

    public void Interact(Interactor interactor)
    {
        interactor.Inventory.Add(itemDefinition);
        
        gameObject.SetActive(false);
    }
}
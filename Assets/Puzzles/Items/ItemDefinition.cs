using UnityEngine;

[CreateAssetMenu(menuName = "Puzzle/Item Definition")]
public class ItemDefinition : ScriptableObject
{
    public string DisplayName;
    public Sprite Icon;
    public Mesh Mesh;
    public Vector3 MeshScale = new Vector3(1.0f, 1.0f, 1.0f);
    public Material Material;
}
using UnityEngine;

[CreateAssetMenu(menuName = "Puzzle/Item Definition")]
public class ItemDefinition : ScriptableObject
{
    public string DisplayName;
    public Sprite Icon;
    public Mesh Mesh;
    public Material Material;
}
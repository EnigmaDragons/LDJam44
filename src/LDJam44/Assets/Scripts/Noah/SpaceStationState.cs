using UnityEngine;

[CreateAssetMenu(menuName = "Space Station")]
public class SpaceStationState : ScriptableObject
{
    public string Name;
    public string Skin;
    public Color Color;
    public float X;
    public float Y;
    public ProductState[] Products;
}

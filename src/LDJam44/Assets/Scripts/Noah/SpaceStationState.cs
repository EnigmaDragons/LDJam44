using UnityEngine;

[CreateAssetMenu(menuName = "Space Station")]
public class SpaceStationState : ScriptableObject
{
    public string Name;
    public string Skin;
    public Color Color;
    public float X;
    public float Y;
    public ProductState[] CheapProducts;
    public ProductState[] ReasonableProducts;
    public ProductState[] ExpensiveProducts;
}

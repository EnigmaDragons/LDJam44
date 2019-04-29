using UnityEngine;

[CreateAssetMenu(menuName = "Space Station")]
public class SpaceStationState : ScriptableObject
{
    public string Name;
    public string Skin;
    public int X;
    public int Y;
    public ProductState[] CheapProducts;
    public ProductState[] ReasonableProducts;
    public ProductState[] ExpensiveProducts;
}

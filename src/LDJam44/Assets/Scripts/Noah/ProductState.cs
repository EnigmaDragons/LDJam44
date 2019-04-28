using UnityEngine;

[CreateAssetMenu(menuName = "Product")]
public class ProductState : ScriptableObject
{
    public string Name;
    public string Description;
    public int MinBuyPrice;
    public int MaxBuyPrice;
    public int MinSellPrice;
    public int MaxSellPrice;
}

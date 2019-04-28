using System.Collections.Generic;
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

    //Generated Content
    public ProductState[] ProductsForSale;
    public Dictionary<string, int> CurrentBuyPrices = new Dictionary<string, int>();
    public Dictionary<string, int> CurrentSellPrices = new Dictionary<string, int>();
}

using System.Collections.Generic;
using UnityEngine;

public class MutableSpaceStation
{
    public string Name;
    public string Skin;
    public Color Color;
    public float X;
    public float Y;
    public ProductState[] Products;

    public ProductState[] ProductsForSale;
    public Dictionary<string, int> CurrentBuyPrices = new Dictionary<string, int>();
    public Dictionary<string, int> CurrentSellPrices = new Dictionary<string, int>();

    public MutableSpaceStation(SpaceStationState spaceStation)
    {
        Name = spaceStation.Name;
        Skin = spaceStation.Skin;
        Color = spaceStation.Color;
        X = spaceStation.X;
        Y = spaceStation.Y;
        Products = spaceStation.Products;
    }
}

using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class SetupStation : MonoBehaviour
{
    public GameObject SpaceStationCanvas;

    void Start()
    {
        var gameState = GameObject.Find("GameState").GetComponent<GameState>();
        var spaceStation = gameState.CurrentSpaceStationData;
        spaceStation.ProductsForSale = new []
        {
            spaceStation.CheapProducts.Random(),
            spaceStation.ReasonableProducts.Random(),
            spaceStation.ExpensiveProducts.Random()
        };
        gameState.PlayerData.Products = gameState.CurrentSpaceStationData.ProductsForSale;
        gameState.PlayerData.Counts = new[] {0, 0, 0};
        spaceStation.ProductsForSale.ToList().ForEach(product =>
        {
            spaceStation.CurrentSellPrices[product.Name] = Random.Range(product.MinSellPrice, product.MaxSellPrice);
            gameState.GalaxyData.Stations.ToList().ForEach(station =>
            {
                station.CurrentBuyPrices[product.Name] = Random.Range(product.MinBuyPrice, product.MaxBuyPrice);
            });
        });
        Instantiate(SpaceStationCanvas);
    }
}

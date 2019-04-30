using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SetupStation : MonoBehaviour
{
    public GameObject SpaceStationCanvas;
    public GameObject SpaceStation;
    public GameObject StationDockedShip;

    void Start()
    {
        var gameState = GameObject.Find("GameState").GetComponent<GameState>();
        var spaceStation = gameState.CurrentSpaceStationData;

        var i = 3;
        for (var _ = 0; i < spaceStation.Products.Length; i++)
            if (gameState.PlayerData.LifeForce < spaceStation.Products[i].MaxSellPrice)
                break;
        spaceStation.ProductsForSale = new[]
        {
            spaceStation.Products[i - 3],
            spaceStation.Products[i - 2],
            spaceStation.Products[i - 1],
        };

        gameState.PlayerData.Products = gameState.CurrentSpaceStationData.ProductsForSale;
        gameState.PlayerData.Counts = new[] {0, 0, 0};
        spaceStation.ProductsForSale.ToList().ForEach(product =>
        {
            spaceStation.CurrentSellPrices[product.Name] = Random.Range(product.MinSellPrice, product.MaxSellPrice + 1);
            gameState.GalaxyData.Stations.ToList().ForEach(station =>
            {
                station.CurrentBuyPrices[product.Name] = (int)Math.Ceiling(Random.Range(product.MinBuyPrice, product.MaxBuyPrice + 1) * gameState.UpgradeEffect("Trading"));
            });
        });
        Instantiate(SpaceStationCanvas);
        var stationGameObject = Instantiate(SpaceStation, new Vector3(0, 0, 0), SpaceStation.gameObject.transform.rotation);
        stationGameObject.GetComponent<SpaceStationSkin>().SpaceStation = spaceStation;
        Instantiate(StationDockedShip, StationDockedShip.transform.position, StationDockedShip.transform.rotation);
    }
}

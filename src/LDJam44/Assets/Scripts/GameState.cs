using System.Linq;
using Assets.Scripts.Noah;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameState : Singleton<GameState>
{
    [SerializeField] private PlayerState PlayerState;
    [SerializeField] private TravelPlanState TravelPlanState;
    [SerializeField] private GalaxyState GalaxyState;

    public MutablePlayer PlayerData;
    public MutableTravelPlan TravelPlanData;
    public MutableGalaxy GalaxyData;

    public MutableSpaceStation CurrentSpaceStationData => GalaxyData.Stations.First(x => x.Name == PlayerData.StationName);
    public MutableSpaceStation TravelingToSpaceStation => GalaxyData.Stations.First(x => x.Name == TravelPlanData.Destination);

    public override void Awake()
    {
        base.Awake();
        if (GalaxyData == null)
            GalaxyData = new MutableGalaxy(GalaxyState);
        if (PlayerData == null)
            PlayerData = new MutablePlayer(PlayerState);
        if (TravelPlanData == null)
            TravelPlanData = new MutableTravelPlan(TravelPlanState);

        //Temp Setup Script
        if (CurrentSpaceStationData.ProductsForSale == null || CurrentSpaceStationData.ProductsForSale.Length == 0)
        {
            var spaceStation = CurrentSpaceStationData;
            spaceStation.ProductsForSale = new[]
            {
                spaceStation.CheapProducts.Random(),
                spaceStation.ReasonableProducts.Random(),
                spaceStation.ExpensiveProducts.Random()
            };
            PlayerData.Products = CurrentSpaceStationData.ProductsForSale;
            PlayerData.Counts = new[] { 0, 0, 0 };
            spaceStation.ProductsForSale.ToList().ForEach(product =>
            {
                spaceStation.CurrentSellPrices[product.Name] = Random.Range(product.MinSellPrice, product.MaxSellPrice);
                GalaxyData.Stations.ToList().ForEach(station =>
                {
                    station.CurrentBuyPrices[product.Name] = Random.Range(product.MinBuyPrice, product.MaxBuyPrice);
                });
            });
        }
    }
}

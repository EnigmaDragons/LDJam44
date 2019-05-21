using System;
using System.Linq;
using Assets.Scripts.Noah;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameState : Singleton<GameState>
{
    [SerializeField] private PlayerState PlayerState;
    [SerializeField] private TravelPlanState TravelPlanState;
    [SerializeField] private GalaxyState GalaxyState;

    public bool SpaceStationTutorialShown = false;
    public bool MapTutorialShown = false;

    public MutablePlayer PlayerData;
    public MutableTravelPlan TravelPlanData;
    public MutableGalaxy GalaxyData;

    public MutableSpaceStation CurrentSpaceStationData => GalaxyData.Stations.First(x => x.Name == PlayerData.StationName);
    public MutableSpaceStation TravelingToSpaceStation => GalaxyData.Stations.First(x => x.Name == TravelPlanData.Destination);
    public float UpgradeEffect(string name) => GalaxyData.Upgrade(name).Effects[PlayerData.UpgradeLevel(name)];

    public override void Awake()
    {
        base.Awake();
        if (GalaxyData == null)
            GalaxyData = new MutableGalaxy(GalaxyState);
        if (PlayerData == null)
            PlayerData = new MutablePlayer(PlayerState, GalaxyState);
        if (TravelPlanData == null)
            TravelPlanData = new MutableTravelPlan(TravelPlanState);

        //Temp Setup Script
        if (CurrentSpaceStationData.ProductsForSale == null || CurrentSpaceStationData.ProductsForSale.Length == 0)
        {
            var spaceStation = CurrentSpaceStationData;

            var i = 3;
            for (var _ = 0; i < spaceStation.Products.Length; i++)
                if (PlayerData.LifeForce < spaceStation.Products[i].MaxSellPrice)
                    break;
            spaceStation.ProductsForSale = new[]
            {
                spaceStation.Products[i - 3],
                spaceStation.Products[i - 2],
                spaceStation.Products[i - 1],
            };

            PlayerData.Products = CurrentSpaceStationData.ProductsForSale;
            PlayerData.Counts = new[] { 0, 0, 0 };
            spaceStation.ProductsForSale.ToList().ForEach(product =>
            {
                spaceStation.CurrentSellPrices[product.Name] = Random.Range(product.MinSellPrice, product.MaxSellPrice + 1);
                GalaxyData.Stations.ToList().ForEach(station =>
                {
                    station.CurrentBuyPrices[product.Name] = (int)Math.Ceiling(Random.Range(product.MinBuyPrice, product.MaxBuyPrice + 1) * UpgradeEffect("Trading"));
                });
            });
        }
    }

    public void Reset()
    {
        GalaxyData = new MutableGalaxy(GalaxyState);
        PlayerData = new MutablePlayer(PlayerState, GalaxyState);
        TravelPlanData = new MutableTravelPlan(TravelPlanState);

        //Temp Setup Script
        if (CurrentSpaceStationData.ProductsForSale == null || CurrentSpaceStationData.ProductsForSale.Length == 0)
        {
            var spaceStation = CurrentSpaceStationData;

            var i = 3;
            for (var _ = 0; i < spaceStation.Products.Length; i++)
                if (PlayerData.LifeForce < spaceStation.Products[i].MaxSellPrice)
                    break;
            spaceStation.ProductsForSale = new[]
            {
                spaceStation.Products[i - 3],
                spaceStation.Products[i - 2],
                spaceStation.Products[i - 1],
            };

            PlayerData.Products = CurrentSpaceStationData.ProductsForSale;
            PlayerData.Counts = new[] { 0, 0, 0 };
            spaceStation.ProductsForSale.ToList().ForEach(product =>
            {
                spaceStation.CurrentSellPrices[product.Name] = Random.Range(product.MinSellPrice, product.MaxSellPrice + 1);
                GalaxyData.Stations.ToList().ForEach(station =>
                {
                    station.CurrentBuyPrices[product.Name] = (int)Math.Ceiling(Random.Range(product.MinBuyPrice, product.MaxBuyPrice + 1) * UpgradeEffect("Trading"));
                });
            });
        }
    }
}

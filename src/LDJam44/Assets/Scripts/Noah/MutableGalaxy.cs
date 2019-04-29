using System.Linq;

public class MutableGalaxy
{
    public MutableSpaceStation[] Stations;

    public int[] ThrusterCosts;
    public int[] StabilizerCosts;
    public int[] TradingCosts;
    public int[] LootingCosts;
    public int[] DroneCosts;
    public int[] AmpCosts;
    public int[] ShieldCosts;
    public int[] DrainCosts;

    public MutableGalaxy(GalaxyState galaxy)
    {
        Stations = galaxy.Stations.Select(x => new MutableSpaceStation(x)).ToArray();
        ThrusterCosts = galaxy.ThrusterCosts;
        StabilizerCosts = galaxy.StabilizerCosts;
        TradingCosts = galaxy.TradingCosts;
        LootingCosts = galaxy.LootingCosts;
        DroneCosts = galaxy.DroneCosts;
        AmpCosts = galaxy.AmpCosts;
        ShieldCosts = galaxy.ShieldCosts;
        DrainCosts = galaxy.DrainCosts;
    }
}

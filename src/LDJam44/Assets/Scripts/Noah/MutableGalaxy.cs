using System.Linq;

public class MutableGalaxy
{
    public MutableSpaceStation[] Stations;
    public UpgradeRules[] Upgrades;

    public MutableGalaxy(GalaxyState galaxy)
    {
        Stations = galaxy.Stations.Select(x => new MutableSpaceStation(x)).ToArray();
        Upgrades = galaxy.Upgrades;
    }

    public UpgradeRules Upgrade(string name) => Upgrades.First(x => x.Name == name);
}

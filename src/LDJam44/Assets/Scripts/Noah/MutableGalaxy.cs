using System.Linq;
using UnityEngine;

public class MutableGalaxy
{
    public MutableSpaceStation[] Stations;
    public UpgradeRules[] Upgrades;

    public MutableGalaxy(GalaxyState galaxy)
    {
        Stations = galaxy.Stations.Select(x => new MutableSpaceStation(x)).ToArray();
        Upgrades = galaxy.Upgrades;
    }

    public UpgradeRules Upgrade(string name)
    {
        var r = Upgrades.FirstOrDefault(x => x.Name == name);
        if (r == null)
            Debug.LogError($"Missing upgrade {name}");
        return r;
    }
}

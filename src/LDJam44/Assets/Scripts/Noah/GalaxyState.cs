using UnityEngine;

[CreateAssetMenu(menuName = "Galaxy")]
public class GalaxyState : ScriptableObject
{
    public SpaceStationState[] Stations;
    public UpgradeRules[] Upgrades;
}

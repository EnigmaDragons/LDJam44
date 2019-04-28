using UnityEngine;

[CreateAssetMenu(menuName = "Galaxy")]
public class GalaxyState : ScriptableObject
{
    public SpaceStationState[] Stations;

    public int[] ThrusterCosts;
    public int[] StabilizerCosts;
    public int[] TradingCosts;
    public int[] LootingCosts;
    public int[] DroneCosts;
    public int[] AmpCosts;
    public int[] ShieldCosts;
    public int[] DrainCosts;
}

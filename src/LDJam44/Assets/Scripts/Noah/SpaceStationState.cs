using UnityEngine;

[CreateAssetMenu(menuName = "SpaceStationRules")]
public class SpaceStationState : ScriptableObject
{
    public int ShipmentUnitCost;
    public int[] ThrusterCosts;
    public int[] StabilizerCosts;
    public int[] TradingCosts;
    public int[] LootingCosts;
    public int[] DroneCosts;
    public int[] AmpCosts;
    public int[] ShieldCosts;
    public int[] DrainCosts;
}

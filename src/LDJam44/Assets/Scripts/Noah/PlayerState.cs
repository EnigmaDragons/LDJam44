using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState")]
public class PlayerState : ScriptableObject
{
    public int LifeForce;
    public int ShipmentUnits;

    public int Thrusters;
    public int Stabilizers;
    public int Trading;
    public int Looting;
    public int Drones;
    public int Amp;
    public int Shields;
    public int Drain;
}

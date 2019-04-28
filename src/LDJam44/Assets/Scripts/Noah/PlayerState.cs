using UnityEngine;

[CreateAssetMenu(menuName = "Player State")]
public class PlayerState : ScriptableObject
{
    public int LifeForce;
    public ProductState[] Products;
    public int[] Counts;
    public string StationName;

    public int Thrusters;
    public int Stabilizers;
    public int Trading;
    public int Looting;
    public int Drones;
    public int Amp;
    public int Shields;
    public int Drain;
}

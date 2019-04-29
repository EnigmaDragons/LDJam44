using UnityEngine;

[CreateAssetMenu(menuName = "Player State")]
public class PlayerState : ScriptableObject
{
    public int LifeForce;
    public string StationName;
    public float HealthScalingCost;
}

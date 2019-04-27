using UnityEngine;

class EnemyFlightPath : ScriptableObject
{
    [SerializeField] GameObject[] waypoints;

    public GameObject[] Waypoints() => waypoints;
}
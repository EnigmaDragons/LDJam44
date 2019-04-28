using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class EnemyWaveSpawner : VerboseMonoBehaviour
{
    [SerializeField] GameObject[] enemyPrototypes = new GameObject[0];
    [SerializeField] Wave wavePrototype;
    [SerializeField] float densityFactor = 12f;
    [SerializeField] float forwardBias = 1.0f;
    [SerializeField] int minEnemiesPerWave = 2;
    [SerializeField] int maxEnemiesPerWave = 7;
    [SerializeField] int minWaypoints = 3;
    [SerializeField] int maxWaypoints = 6;
    [SerializeField] float minWaypointDistance = 15f;
    [SerializeField] float minAheadOfPlayer = 26f;
    [SerializeField] float maxAheadOfPlayer = 50f;
    [SerializeField] float minSecondsBetweenEnemies = 0.5f;
    [SerializeField] float maxSecondsBetweenEnemies = 1.5f;

    public void Init(LevelSettings settings)
    {
        var maxZ = settings.TravelDistance - SpawnBoundaries.endClearPlayAreaDistance;
        for (var z = SpawnBoundaries.startClearPlayAreaDistance; z < maxZ; z += 180 * (1 / densityFactor))
            SpawnWave(maxZ, z, settings);
    }

    private void SpawnWave(float maxZ, float z, LevelSettings settings)
    {
        var shouldStartForward = Random.Range(0f, 1f) <= forwardBias;
        var startZ = shouldStartForward
            ? z + Random.Range(minAheadOfPlayer, maxAheadOfPlayer)
            : z - 10f - Random.Range(-5f, 5f);

        if (startZ > maxZ)
            return;

        var startPosition = SpawnBoundaries.RandomOffPlayZone(startZ);
        var firstWaypoint = new Vector3(startPosition.x, startPosition.y, startZ - z);

        var waypoints = new List<Vector3>();
        waypoints.Add(firstWaypoint);

        var numMidWaypoints = Random.Range(minWaypoints, maxWaypoints) - 2;
        for (var i = 0; i < numMidWaypoints; i++)
            waypoints.Add(NextSaneWaypoint(waypoints.Last()));

        var endPosition = SpawnBoundaries.RandomOffPlayZone(waypoints.Last().z, 5f);
        waypoints.Add(endPosition * 1.5f);

        var waveConfig = new WaveConfig
        {
            EnemyProtoype = enemyPrototypes.Random(),
            Path = waypoints.ToArray(),
            NumEnemies = Random.Range(minEnemiesPerWave, maxEnemiesPerWave),
            SecondsBetweenEnemies = Random.Range(minSecondsBetweenEnemies, maxSecondsBetweenEnemies),
            ZTriggerThreshold = z,
        };

        var w = Instantiate(wavePrototype, startPosition, Quaternion.identity);
        w.Init(waveConfig);
    }

    private Vector3 NextSaneWaypoint(Vector3 lastWaypoint)
    {
        var result = SpawnBoundaries.RandomInPlayZone(lastWaypoint.z, 20f);
        while(Mathf.Abs(Vector3.Distance(lastWaypoint, result)) < minWaypointDistance)
            result = SpawnBoundaries.RandomInPlayZone(lastWaypoint.z, 20f);
        return result;
    }
}

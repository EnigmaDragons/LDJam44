using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class EnemyWaveSpawner : VerboseMonoBehaviour
{
    [SerializeField] GameObject[] enemyPrototypes = new GameObject[0];
    [SerializeField] private GameObject Mothership;
    [SerializeField] Wave wavePrototype;
    [SerializeField] float densityFactor = 1f;
    [SerializeField] float baseDensity = 4f;
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
        Debug.Log("Difficulty: " + settings.Difficulty);
        minEnemiesPerWave = settings.Difficulty - 1;
        maxEnemiesPerWave = settings.Difficulty + 1;
        var computedDensityFactor = settings.Difficulty * densityFactor + baseDensity;

        var maxZ = settings.TravelDistance - SpawnBoundaries.endClearPlayAreaDistance;
        var density = 1000 * (1 / computedDensityFactor);
        if (settings.Difficulty > 5)
            SpawnBoss(maxZ + 50, 100);
        for (var z = SpawnBoundaries.startClearPlayAreaDistance; z < maxZ; z += density)
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

        var numMidWaypoints = Random.Range(minWaypoints, maxWaypoints + 1) - 2;
        for (var i = 0; i < numMidWaypoints; i++)
            waypoints.Add(NextSaneWaypoint(waypoints.Last()));

        var endPosition = SpawnBoundaries.RandomOffPlayZone(waypoints.Last().z, 5f);
        waypoints.Add(endPosition * 1.5f);

        var waveConfig = new WaveConfig
        {
            EnemyProtoype = enemyPrototypes.Random(),
            Path = waypoints.ToArray(),
            NumEnemies = Random.Range(minEnemiesPerWave, maxEnemiesPerWave + 1),
            SecondsBetweenEnemies = Random.Range(minSecondsBetweenEnemies, maxSecondsBetweenEnemies),
            ZTriggerThreshold = z,
            MaxZAllowed = maxZ
        };

        var w = Instantiate(wavePrototype, startPosition, Quaternion.identity);
        w.Init(waveConfig);
    }

    private void SpawnBoss(float maxZ, float z)
    {
        var shouldStartForward = Random.Range(0f, 1f) <= forwardBias;
        var startZ = z + maxAheadOfPlayer + 50;

        var startPosition = SpawnBoundaries.RandomOffPlayZone(startZ);
        var firstWaypoint = new Vector3(startPosition.x, startPosition.y, startZ - z);

        var waypoints = new List<Vector3>();
        waypoints.Add(firstWaypoint);

        var numMidWaypoints = 99;
        for (var i = 0; i < numMidWaypoints; i++)
            waypoints.Add(NextSaneWaypoint(waypoints.Last()));

        var endPosition = SpawnBoundaries.RandomOffPlayZone(waypoints.Last().z, 5f);
        waypoints.Add(endPosition * 1.5f);

        var waveConfig = new WaveConfig
        {
            EnemyProtoype = Mothership,
            Path = waypoints.ToArray(),
            NumEnemies = 1,
            SecondsBetweenEnemies = 1,
            ZTriggerThreshold = z,
            MaxZAllowed = maxZ
        };

        var w = Instantiate(wavePrototype, startPosition, Quaternion.identity);
        w.Init(waveConfig);
    }

    private Vector3 NextSaneWaypoint(Vector3 lastWaypoint)
    {
        var zVariance = 40;
        var result = SpawnBoundaries.RandomInPlayZone(lastWaypoint.z, zVariance);
        var maxTries = 20;
        for(var i = 0; Mathf.Abs(Vector3.Distance(lastWaypoint, result)) < minWaypointDistance || i > maxTries; i++)
            result = SpawnBoundaries.RandomInPlayZone(lastWaypoint.z, zVariance);
        return result;
    }
}

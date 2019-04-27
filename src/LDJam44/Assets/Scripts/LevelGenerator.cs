using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // Settings
    [SerializeField] float travelDistance = 95f;
    [SerializeField] int difficulty = 3;

    // Object Prototypes
    [SerializeField] GameObject levelEnd;
    [SerializeField] GameObject spaceStation;

    // Spawners
    [SerializeField] AsteroidSpawner asteroids;

    void Start()
    {
        var settings = new LevelSettings { TravelDistance = travelDistance, Difficulty = difficulty };
        Instantiate(levelEnd, new Vector3(0, 0, travelDistance), Quaternion.identity);
        Instantiate(spaceStation, new Vector3(0, 0, travelDistance + 5f), Quaternion.identity);
        asteroids.Init(settings);
    }
}

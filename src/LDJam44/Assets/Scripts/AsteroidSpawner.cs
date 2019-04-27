using UnityEngine;

class AsteroidSpawner : VerboseMonoBehaviour
{
    [SerializeField] GameObject[] asteroidPrototypes;
    [SerializeField] float densityFactor = 12;

    public void Init(LevelSettings settings)
    {
        var maxZ = settings.TravelDistance * 1.15f;
        for (var z = 0f; z < maxZ; z += 5)
            SpawnAsteroid(z);
    }

    private void SpawnAsteroid(float z)
    {
        Debug.Log($"Spawn Asteroid {z}");
        var selectedAsteroid = Random.Range(0, asteroidPrototypes.Length);
        var position = SpawnBoundaries.RandomInLevel(z);
        Instantiate(asteroidPrototypes[selectedAsteroid], position, Quaternion.identity);
    }
}

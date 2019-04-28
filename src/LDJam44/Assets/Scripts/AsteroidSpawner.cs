using UnityEngine;

class AsteroidSpawner : VerboseMonoBehaviour
{
    [SerializeField] GameObject[] asteroidPrototypes;
    [SerializeField] float densityFactor = 12f;
    [SerializeField] float clearPlayAreaDistance = 38f;

    public void Init(LevelSettings settings)
    {
        var maxZ = settings.TravelDistance * 1.15f;
        for (var z = 0f; z < maxZ; z += 20 * (1 / densityFactor))
            SpawnAsteroid(z, settings.Difficulty);
    }

    private void SpawnAsteroid(float z, int difficulty)
    {
        Debug.Log($"Spawn Asteroid {z}");
        var selectedAsteroid = Random.Range(0, asteroidPrototypes.Length);
        var shouldBeObstacle = z >= clearPlayAreaDistance && Random.Range(0, 10) < difficulty;

        var position = shouldBeObstacle 
            ? SpawnBoundaries.RandomInPlayZone(z) 
            : SpawnBoundaries.RandomOffPlayZone(z);

        var scale = Random.Range(asteroidPrototypes[selectedAsteroid].transform.localScale.x / 4, asteroidPrototypes[selectedAsteroid].transform.localScale.x * 2);
        var o = Instantiate(asteroidPrototypes[selectedAsteroid], position, Quaternion.identity);
        o.transform.localScale += new Vector3(scale, scale, scale);
    }
}

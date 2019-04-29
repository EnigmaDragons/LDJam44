using UnityEngine;

class AsteroidSpawner : VerboseMonoBehaviour
{
    [SerializeField] GameObject[] asteroidPrototypes = new GameObject[0];
    [SerializeField] float densityFactor = 12f;
    [SerializeField] float decorDensityFactor = 12f;

    public void Init(LevelSettings settings)
    {
        var maxZ = settings.TravelDistance + 100;
        for (var z = 0f; z < maxZ; z += 40 * (1 / densityFactor))
            SpawnObstacleAsteroid(z, settings);
    }

    public void SpawnDecor(LevelSettings settings)
    {
        var maxZ = settings.TravelDistance + 100;
        for (var z = 0f; z < maxZ; z += 20 * (1 / decorDensityFactor))
            SpawnDecorAsteroid(z);
    }

    public void SpawnDecorAsteroid(float z) => SpawnAsteroid(z, false);

    public void SpawnObstacleAsteroid(float z, LevelSettings settings)
    {
        var shouldBeObstacle = z >= SpawnBoundaries.startClearPlayAreaDistance
            && z <= settings.TravelDistance - SpawnBoundaries.endClearPlayAreaDistance
            && Random.Range(0, 10) < settings.Difficulty;

        if (shouldBeObstacle)
            SpawnAsteroid(z, shouldBeObstacle);
    }

    public void SpawnAsteroid(float z, bool isObstacle)
    {
        var selectedAsteroid = Random.Range(0, asteroidPrototypes.Length);
        var position = isObstacle
            ? SpawnBoundaries.RandomInPlayZone(z)
            : SpawnBoundaries.RandomDecorZone(z);

        var prototypeScale = asteroidPrototypes[selectedAsteroid].transform.localScale.x;
        var sizeAdjustment = Random.Range(-prototypeScale * 0.75f, prototypeScale * 2.4f);
        var o = Instantiate(asteroidPrototypes[selectedAsteroid], position, Quaternion.identity);
        o.transform.localScale += new Vector3(sizeAdjustment, sizeAdjustment, sizeAdjustment);
        o.transform.rotation = Quaternion.Euler(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f));
    }
}

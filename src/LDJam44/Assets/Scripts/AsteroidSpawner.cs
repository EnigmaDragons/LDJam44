using UnityEngine;

class AsteroidSpawner : VerboseMonoBehaviour
{
    [SerializeField] GameObject[] asteroidPrototypes = new GameObject[0];
    [SerializeField] float densityFactor = 12f;
    [SerializeField] float startClearPlayAreaDistance = 38f;
    [SerializeField] float endClearPlayAreaDistance = 10f;

    public void Init(LevelSettings settings)
    {
        var maxZ = settings.TravelDistance + 50;
        for (var z = 0f; z < maxZ; z += 40 * (1 / densityFactor))
            SpawnAsteroid(z, settings);
    }

    private void SpawnAsteroid(float z, LevelSettings settings)
    {
        var selectedAsteroid = Random.Range(0, asteroidPrototypes.Length);
        var shouldBeObstacle = z >= startClearPlayAreaDistance
            && z <= settings.TravelDistance - endClearPlayAreaDistance 
            && Random.Range(0, 10) < settings.Difficulty;

        var position = shouldBeObstacle 
            ? SpawnBoundaries.RandomInPlayZone(z) 
            : SpawnBoundaries.RandomOffPlayZone(z);

        var prototypeScale = asteroidPrototypes[selectedAsteroid].transform.localScale.x;
        var sizeAdjustment = Random.Range(-prototypeScale * 0.75f, prototypeScale * 2.4f);
        var o = Instantiate(asteroidPrototypes[selectedAsteroid], position, Quaternion.identity);
        o.transform.localScale += new Vector3(sizeAdjustment, sizeAdjustment, sizeAdjustment);
    }
}

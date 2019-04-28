using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // Object Prototypes
    [SerializeField] GameObject levelEnd;
    [SerializeField] GameObject spaceStation;

    // Spawners
    [SerializeField] AsteroidSpawner asteroids;
    [SerializeField] EnemyWaveSpawner enemies;

    private void Awake()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Random.Range(0f, 360f));
    }

    private void Start()
    {
        var travelPlan = GameObject.Find("GameState").GetComponent<GameState>().TravelPlanData;
        Instantiate(levelEnd, new Vector3(0, 0, travelPlan.Distance), Quaternion.identity);
        Instantiate(spaceStation, new Vector3(0, 0, travelPlan.Distance + 5f), Quaternion.identity);
        var settings = new LevelSettings { TravelDistance = travelPlan.Distance, Difficulty = travelPlan.Difficulty };
        asteroids.Init(settings);
        enemies.Init(settings);
    }
}

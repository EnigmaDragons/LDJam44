using System.Linq;
using UnityEngine;

public class LevelGenerator : VerboseMonoBehaviour
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
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogStartDistance = 300f;
        RenderSettings.fogEndDistance = 1600f;
        RenderSettings.fogColor = Color.black;
        RenderSettings.fogDensity = 150f;
    }

    private void Start()
    {
        var s = Find("GameState").GetComponent<GameState>();
        Debug.Log($"Player Health is {s.PlayerData.Health}");
        Find("Spaceship").GetComponent<Health>().Init(s.PlayerData.Health);

        SetupLevelEnd(s);

        CreateChallenges(s);
    }

    private void SetupLevelEnd(GameState s)
    {
        var travelDistance = s.TravelPlanData.Distance;
        Instantiate(levelEnd, new Vector3(0, 0, travelDistance), Quaternion.identity);
        var station = Instantiate(spaceStation, new Vector3(0, -3, travelDistance + 30f), Quaternion.Euler(spaceStation.transform.eulerAngles.x, Random.Range(-180, 180), Random.Range(-180, 180)));
        station.GetComponent<SpaceStationSkin>().SpaceStation = GameObject.Find("GameState").GetComponent<GameState>().TravelingToSpaceStation;
        station.transform.localScale = new Vector3(25, 25, 25);
    }

    private void CreateChallenges(GameState s)
    {
        var settings = new LevelSettings { TravelDistance = s.TravelPlanData.Distance, Difficulty = s.TravelPlanData.Difficulty };
        asteroids.Init(settings);
        asteroids.SpawnDecor(settings);
        enemies.Init(settings);
    }
}

using Assets.Scripts;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    // Object Prototypes
    [SerializeField] GameObject levelEnd;
    [SerializeField] GameObject spaceStation;

    // Spawners
    [SerializeField] AsteroidSpawner asteroids;

    private void Awake()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Random.Range(0f, 360f));
    }

    private void Start()
    {
        var travelPlan = GameObject.Find("GameState").GetComponent<GameState>().TravelPlanData;
        Instantiate(levelEnd, new Vector3(0, 0, travelPlan.Distance), Quaternion.identity);
        var station = Instantiate(spaceStation, new Vector3(0, -10, travelPlan.Distance + 15f), Quaternion.Euler(spaceStation.transform.eulerAngles.x, Random.Range(-180, 180), Random.Range(-180, 180)));
        station.GetComponent<SpaceStationSkin>().IsTravelingTo = true;
        station.transform.localScale = new Vector3(25, 25, 25);
        var settings = new LevelSettings { TravelDistance = travelPlan.Distance, Difficulty = travelPlan.Difficulty };
        asteroids.Init(settings);
    }
}

using System.Linq;
using UnityEngine;

public class Map : MonoBehaviour
{
    private GameState gameState;

    public GameObject Tutorial;
    public GameObject SpaceStation;
    public GameObject SpaceStationMapElementUI;
    public GameObject SpaceShip;

    private GameObject spaceship;

    void Start()
    {
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
        if (gameState.MapTutorialShown)
            Init();
        else
            Tutorial.SetActive(true);
    }

    void Init()
    {
        Tutorial.SetActive(false);
        spaceship = Instantiate(SpaceShip, new Vector3(gameState.CurrentSpaceStationData.X, 1, gameState.CurrentSpaceStationData.Y), SpaceShip.transform.rotation);
        gameState.GalaxyData.Stations.ToList().ForEach(x =>
        {
            var station = Instantiate(SpaceStation, new Vector3(x.X, 0, x.Y), Quaternion.Euler(SpaceStation.transform.eulerAngles.x, Random.Range(-180, 180), Random.Range(-180, 180)));
            station.transform.localScale = new Vector3(1, 1, 1);
            station.GetComponent<SpaceStationSkin>().SpaceStation = x;
            var ui = Instantiate(SpaceStationMapElementUI, transform).GetComponent<MapStationUI>();
            ui.SpaceStation = x;
            ui.GameState = gameState;
            ui.SpaceShip = spaceship.GetComponent<FlyToStation>();
            ui.SpaceStationObject = station;
        });
    }

    public void DismissTutorial()
    {
        gameState.MapTutorialShown = true;
        Init();
    }
}

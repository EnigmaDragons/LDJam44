using System.Linq;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject SpaceStation;
    public GameObject SpaceStationMapElementUI;

    void Start()
    {
        var gameState = GameObject.Find("GameState").GetComponent<GameState>();
        gameState.GalaxyData.Stations.ToList().ForEach(x =>
        {
            var station = Instantiate(SpaceStation, new Vector3(x.X, 0, x.Y), Quaternion.Euler(SpaceStation.transform.eulerAngles.x, Random.Range(-180, 180), Random.Range(-180, 180)));
            station.transform.localScale = new Vector3(1, 1, 1);
            station.GetComponent<SpaceStationSkin>().SpaceStation = x;
            var ui = Instantiate(SpaceStationMapElementUI, transform).GetComponent<MapStationUI>();
            ui.SpaceStation = x;
            ui.GameState = gameState;
        });
    }
}

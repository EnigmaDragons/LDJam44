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
            Instantiate(SpaceStation, new Vector3(x.X, 0, x.Y), Quaternion.identity);
            var ui = Instantiate(SpaceStationMapElementUI, transform).GetComponent<MapStationUI>();
            ui.SpaceStation = x;
            ui.GameState = gameState;
        });
    }
}

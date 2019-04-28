using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpaceStationSkin : MonoBehaviour
{
    public MeshRenderer MeshRenderer;
    public List<Material> Materials;
    public bool IsTravelingTo;

    void Start()
    {
        var gameState = GameObject.Find("GameState").GetComponent<GameState>();
        var skin = IsTravelingTo
            ? gameState.GalaxyData.Stations.First(station => station.Name == gameState.TravelPlanData.Destination).Skin
            : gameState.CurrentSpaceStationData.Skin;
        MeshRenderer.material = Materials.First(x => x.name == skin);
    }
}

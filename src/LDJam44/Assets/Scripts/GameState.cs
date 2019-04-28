using System.Linq;
using Assets.Scripts.Noah;
using UnityEngine;

public class GameState : Singleton<GameState>
{
    [SerializeField] public PlayerState PlayerState;
    [SerializeField] public TravelPlanState TravelPlanState;

    public MutablePlayer PlayerData;
    public MutableTravelPlan TravelPlanData;
    [SerializeField] public GalaxyState GalaxyData;


    public SpaceStationState CurrentSpaceStationData => GalaxyData.Stations.First(x => x.Name == PlayerData.StationName);

    public override void Awake()
    {
        base.Awake();
        if (PlayerData == null)
            PlayerData = new MutablePlayer(PlayerState);
        if (TravelPlanData == null)
            TravelPlanData = new MutableTravelPlan(TravelPlanState);
    }
}

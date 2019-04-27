
using UnityEngine;

namespace Assets.Scripts
{
    class GameState : Singleton<GameState>
    {
        [SerializeField] public PlayerState PlayerData;
        [SerializeField] public SpaceStationState SpaceStationData;
        [SerializeField] public TravelPlanState TravelPlanData;
    }
}

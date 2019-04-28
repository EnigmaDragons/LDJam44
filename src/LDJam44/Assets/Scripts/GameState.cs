
using System.Linq;
using Assets.Scripts.Noah;
using UnityEngine;

namespace Assets.Scripts
{
    class GameState : Singleton<GameState>
    {
        [SerializeField] public PlayerState PlayerState;

        public MutablePlayer PlayerData;
        [SerializeField] public GalaxyState GalaxyData;
        [SerializeField] public TravelPlanState TravelPlanData;

        public SpaceStationState CurrentSpaceStationData => GalaxyData.Stations.First(x => x.Name == PlayerData.StationName);

        public override void Awake()
        {
            base.Awake();
            if (PlayerData == null)
                PlayerData = new MutablePlayer(PlayerState);
        }
    }
}

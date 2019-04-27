
using UnityEngine;

namespace Assets.Scripts
{
    class GameState : Singleton<GameState>
    {
        [SerializeField] public PlayerState PlayerData;
    }
}

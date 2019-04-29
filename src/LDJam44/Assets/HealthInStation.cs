using Assets.Scripts.Noah;
using TMPro;
using UnityEngine;

public class HealthInStation : VerboseMonoBehaviour
{
    private MutablePlayer player;
    private TextMeshProUGUI text;

    private void Start()
    {
        text = VerboseGetComponent<TextMeshProUGUI>();
        player = GameObject.Find("GameState").GetComponent<GameState>().PlayerData;
    }

    void Update()
    {
        text.text = $"{player.Health} / {player.Health}";
    }
}

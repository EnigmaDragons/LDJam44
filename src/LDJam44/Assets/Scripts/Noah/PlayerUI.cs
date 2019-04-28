using Assets.Scripts;
using Assets.Scripts.Noah;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private MutablePlayer player;

    public Text LifeForce;
    public Text CheapProduct;
    public Text ReasonableProduct;
    public Text ExpensiveProduct;

    public void Start()
    {
        player = GameObject.Find("GameState").GetComponent<GameState>().PlayerData;
    }

    public void Update()
    {
        LifeForce.text = player.LifeForce + " Life Force";
        CheapProduct.text = $"{player.Products[0].Name}: {player.Counts[0]}";
        ReasonableProduct.text = $"{player.Products[1].Name}: {player.Counts[1]}";
        ExpensiveProduct.text = $"{player.Products[2].Name}: {player.Counts[2]}";
    }
}

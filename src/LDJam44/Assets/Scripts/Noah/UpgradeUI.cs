using Assets.Scripts.Noah;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    private MutablePlayer player;
    private bool isMaxed;
    private int cost;

    public UpgradeRules Upgrade;
    public Text Level;
    public Text Name;
    public Text Description;
    public Text Cost;
    public Button BuyUpgrade;
    
    void Start()
    {
        player = GameObject.Find("GameState").GetComponent<GameState>().PlayerData;
        isMaxed = player.UpgradeLevel(Upgrade.name) == Upgrade.Costs.Length;
        cost = isMaxed ? 0 : Upgrade.Costs[player.UpgradeLevel(Upgrade.name)];
        Level.text = $"{player.UpgradeLevel(Upgrade.name)}/{Upgrade.Costs.Length}";
        Name.text = Upgrade.Name;
        Description.text = Upgrade.Description;
        Cost.text = isMaxed ? "MAXED" : $"{cost} LF";
        Update();
    }

    void Update()
    {
        var canBuy = !isMaxed && player.LifeForce > cost;
        BuyUpgrade.gameObject.SetActive(canBuy);
    }

    public void Buy()
    {
        player.LifeForce -= cost;
        player.Upgrades[Upgrade.name]++;
        Start();
    }
}

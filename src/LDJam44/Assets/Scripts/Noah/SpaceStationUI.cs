using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpaceStationUI : MonoBehaviour
{
    private SpaceStationState _spaceStationState;
    private PlayerState player;

    public Text ErrorText;
    public Text CreditsText;
    public GameObject MainUI;
    public GameObject UpgradesUI;
    public Text ShipmentUnitsText;
    public Text ShipmentCostText;

    void Start()
    {
        var gameState = GameObject.Find("GameState").GetComponent<GameState>();
        _spaceStationState = gameState.SpaceStationData;
        player = gameState.PlayerData;
        ShipmentCostText.text = "Cost: " + _spaceStationState.ShipmentUnitCost;
    }

    void Update()
    {
        CreditsText.text = player.LifeForce.ToString() + " Life Force";
        ShipmentUnitsText.text = player.ShipmentUnits.ToString();
    }

    public void ReduceShipment()
    {
        if (player.ShipmentUnits == 0)
            ErrorText.text = "No shipments to sell";
        else
        {
            player.LifeForce += _spaceStationState.ShipmentUnitCost;
            player.ShipmentUnits--;
        }
    }

    public void AddShipment()
    {
        if (player.LifeForce <= _spaceStationState.ShipmentUnitCost)
            ErrorText.text = "You must keep at least 1 Life Force remaining";
        else
        {
            player.LifeForce -= _spaceStationState.ShipmentUnitCost;
            player.ShipmentUnits++;
        }
    }

    public void Done()
    {
        SceneManager.LoadScene(SceneNames.ShipTravel);
    }

    public void GoToUpgrades()
    {
        MainUI.SetActive(false);
        UpgradesUI.SetActive(true);
    }

    public void LeaveUpgrades()
    {
        UpgradesUI.SetActive(false);
        MainUI.SetActive(true);
    }
}

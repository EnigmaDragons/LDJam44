using UnityEngine;
using UnityEngine.UI;

public class SpaceStationUI : MonoBehaviour
{
    public string SpaceStationName;
    public string PlayerName;

    private SpaceStation spaceStation;
    private Player player;

    public Text ErrorText;
    public Text CreditsText;
    public Text ShipmentUnitsText;
    public Text ShipmentCostText;

    void Start()
    {
        spaceStation = GameObject.Find(SpaceStationName).GetComponent<SpaceStation>();
        player = GameObject.Find(PlayerName).GetComponent<Player>();
        ShipmentCostText.text = "Cost: " + spaceStation.ShipmentUnitCost;
    }

    void Update()
    {
        CreditsText.text = player.LifeForce.ToString();
        ShipmentUnitsText.text = player.ShipmentUnits.ToString();
    }

    public void ReduceShipment()
    {
        if (player.ShipmentUnits == 0)
            ErrorText.text = "No shipments to sell";
        else
        {
            player.LifeForce += spaceStation.ShipmentUnitCost;
            player.ShipmentUnits--;
        }
    }

    public void AddShipment()
    {
        if (player.LifeForce < spaceStation.ShipmentUnitCost)
            ErrorText.text = "You must keep at least 1 Life Force remaining";
        else
        {
            player.LifeForce -= spaceStation.ShipmentUnitCost;
            player.ShipmentUnits++;
        }
    }
}

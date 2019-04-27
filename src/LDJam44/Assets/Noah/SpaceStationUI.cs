using UnityEngine;
using UnityEngine.UI;

public class SpaceStationUI : MonoBehaviour
{
    public SpaceStation SpaceStation;
    public Player Player;

    public Text ErrorText;
    public Text CreditsText;
    public Text ShipmentUnitsText;
    public Text ShipmentCostText;
    public Button ReduceShipmentButton;
    public Button AddShipmentButton;

    void Start()
    {
        ShipmentCostText.text = "Cost: " + SpaceStation.ShipmentUnitCost;
    }

    void Update()
    {
        CreditsText.text = Player.Credits.ToString();
        ShipmentUnitsText.text = Player.ShipmentUnits.ToString();
    }

    public void ReduceShipment()
    {
        if (Player.ShipmentUnits == 0)
            ErrorText.text = "No shipments to sell";
        else
        {
            Player.Credits += SpaceStation.ShipmentUnitCost;
            Player.ShipmentUnits--;
        }
    }

    public void AddShipment()
    {
        if (Player.Credits < SpaceStation.ShipmentUnitCost)
            ErrorText.text = "Not enough credits";
        else
        {
            Player.Credits -= SpaceStation.ShipmentUnitCost;
            Player.ShipmentUnits++;
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSummary : MonoBehaviour
{
    public string SpaceStationName;
    public string PlayerName;

    private SpaceStation spaceStation;
    private Player player;
    private int shipmentProfit;
    private int total;

    private float timeSpent;

    public int AnimationMilliseconds;
    public Text Shipment;
    public Text ShipmentNum;
    public Text RemainingLifeForce;
    public Text RemainingLifeForceNum;
    public GameObject Line;
    public Text Total;
    public Text TotalNum;
    public Button Ok;

    void Start()
    {
        spaceStation = GameObject.Find(SpaceStationName).GetComponent<SpaceStation>();
        player = GameObject.Find(PlayerName).GetComponent<Player>();
        shipmentProfit = player.ShipmentUnits * spaceStation.ShipmentUnitCost * 2;
        ShipmentNum.text = shipmentProfit.ToString();
        RemainingLifeForceNum.text = player.LifeForce.ToString();
        total = shipmentProfit + player.LifeForce;
        TotalNum.text = total.ToString();
        player.ShipmentUnits = 0;
        player.LifeForce = total;
    }

    void Update()
    {
        timeSpent += Time.deltaTime;
        if (timeSpent * 1000 >= AnimationMilliseconds)
        {
            Shipment.gameObject.SetActive(true);
            ShipmentNum.gameObject.SetActive(true);
        }
        if (timeSpent * 1000 >= AnimationMilliseconds * 2)
        {
            RemainingLifeForce.gameObject.SetActive(true);
            RemainingLifeForceNum.gameObject.SetActive(true);
        }
        if (timeSpent * 1000 >= AnimationMilliseconds * 3)
        {
            Line.gameObject.SetActive(true);
            Total.gameObject.SetActive(true);
            TotalNum.gameObject.SetActive(true);
            Ok.gameObject.SetActive(true);
        }
    }

    public void Done()
    {
        SceneManager.LoadScene("SpaceStation");
    }
}

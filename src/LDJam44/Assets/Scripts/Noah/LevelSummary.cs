using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSummary : MonoBehaviour
{
    public string SpaceStationName;
    public string PlayerName;

    private SpaceStation spaceStation;
    private PlayerState player;
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
        player = GameObject.Find(PlayerName).GetComponent<PlayerState>();
        shipmentProfit = player.ShipmentUnits * spaceStation.ShipmentUnitCost * 2;
        ShipmentNum.text = shipmentProfit.ToString();
        RemainingLifeForceNum.text = player.LifeForce.ToString();
        total = shipmentProfit + player.LifeForce;
        TotalNum.text = total.ToString();
        player.ShipmentUnits = 0;
        player.LifeForce = total;
        StartCoroutine(PresentElements());
    }

    public void Done()
    {
        if (total < 1000000)
            SceneManager.LoadScene(SceneNames.SpaceStationScene);
        else
            SceneManager.LoadScene(SceneNames.WinScene);
    }

    private IEnumerator PresentElements()
    {
        yield return new WaitForSeconds(AnimationMilliseconds / 1000);
        Shipment.gameObject.SetActive(true);
        ShipmentNum.gameObject.SetActive(true);
        yield return new WaitForSeconds(AnimationMilliseconds / 1000);
        RemainingLifeForce.gameObject.SetActive(true);
        RemainingLifeForceNum.gameObject.SetActive(true);
        yield return new WaitForSeconds(AnimationMilliseconds / 1000);
        Line.gameObject.SetActive(true);
        Total.gameObject.SetActive(true);
        TotalNum.gameObject.SetActive(true);
        Ok.gameObject.SetActive(true);
    }
}

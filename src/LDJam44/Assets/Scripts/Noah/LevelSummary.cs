using System;
using System.Collections;
using System.Linq;
using Assets.Scripts.Noah;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSummary : MonoBehaviour
{
    private MutablePlayer player;

    public int AnimationMilliseconds;
    public Text Shipment;
    public Text ShipmentNum;
    public Text RemainingLifeForce;
    public Text RemainingLifeForceNum;
    public Text Loot;
    public Text LootNum;
    public GameObject Line;
    public Text Total;
    public Text TotalNum;
    public Button Ok;

    void Start()
    {
        var gameState = GameObject.Find("GameState").GetComponent<GameState>();
        var playerHealth = FindObjectOfType<Ship>().GetComponent<Health>();
        player = gameState.PlayerData;
        player.LifeForce = (int)Math.Ceiling((double)player.LifeForce * playerHealth.currentHp / playerHealth.maxHp);

        var destinationStation = gameState.GalaxyData.Stations.First(x => x.Name == gameState.TravelPlanData.Destination);
        var shipmentProfit = destinationStation.CurrentBuyPrices[player.Products[0].Name] * player.Counts[0] 
            + destinationStation.CurrentBuyPrices[player.Products[1].Name] * player.Counts[1]
            + destinationStation.CurrentBuyPrices[player.Products[2].Name] * player.Counts[2];
        var total = shipmentProfit + player.LifeForce + player.Loot;

        ShipmentNum.text = shipmentProfit.ToString();
        RemainingLifeForceNum.text = player.LifeForce.ToString();
        LootNum.text = player.Loot.ToString();
        TotalNum.text = total.ToString();
        StartCoroutine(PresentElements());

        player.Loot = 0;
        player.LifeForce = total;
        player.StationName = gameState.TravelPlanData.Destination;
        player.RecaluclateHealth();
    }

    public void Done()
    {
        if (player.LifeForce < 1000000)
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
        Loot.gameObject.SetActive(true);
        LootNum.gameObject.SetActive(true);
        yield return new WaitForSeconds(AnimationMilliseconds / 1000);
        Line.gameObject.SetActive(true);
        Total.gameObject.SetActive(true);
        TotalNum.gameObject.SetActive(true);
        Ok.gameObject.SetActive(true);
    }
}

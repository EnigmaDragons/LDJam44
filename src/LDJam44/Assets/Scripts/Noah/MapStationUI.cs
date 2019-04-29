using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapStationUI : MonoBehaviour
{
    public MutableSpaceStation SpaceStation;
    public GameState GameState;

    private int distance;

    public Text Name;
    public Text Distance;
    public Text Product0;
    public Text Product1;
    public Text Product2;
    public Button TravelButton;

    void Start()
    {
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(SpaceStation.X * 78, SpaceStation.Y * 78, 0);
        Name.text = SpaceStation.Name;
        distance = 50 * (int)Math.Ceiling(Vector2.Distance(new Vector2(SpaceStation.X, SpaceStation.Y), new Vector2(GameState.CurrentSpaceStationData.X, GameState.CurrentSpaceStationData.Y)));
        if (SpaceStation != GameState.CurrentSpaceStationData)
        {
            Distance.text = $"Distance: {distance}";
            Product0.text = $"{GameState.PlayerData.Products[0].Name}: {SpaceStation.CurrentBuyPrices[GameState.PlayerData.Products[0].Name]} LF";
            Product1.text = $"{GameState.PlayerData.Products[1].Name}: {SpaceStation.CurrentBuyPrices[GameState.PlayerData.Products[1].Name]} LF";
            Product2.text = $"{GameState.PlayerData.Products[2].Name}: {SpaceStation.CurrentBuyPrices[GameState.PlayerData.Products[2].Name]} LF";
            TravelButton.gameObject.SetActive(true);
        }
    }

    public void Travel()
    {
        GameState.TravelPlanData.Destination = SpaceStation.Name;
        GameState.TravelPlanData.Distance = distance;
        SceneManager.LoadScene(SceneNames.ShipTravel);
    }
}

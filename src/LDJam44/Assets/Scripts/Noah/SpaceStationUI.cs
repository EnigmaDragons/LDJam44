using Assets.Scripts;
using Assets.Scripts.Noah;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpaceStationUI : MonoBehaviour
{
    private MutableSpaceStation spaceStation;
    private MutablePlayer player;

    public Text ErrorText;
    public Text StationName;
    public GameObject MainUI;
    public GameObject UpgradesUI;
    public Text Product0Name;
    public Text Product0Description;
    public Text Product0Cost;
    public Text Product1Name;
    public Text Product1Description;
    public Text Product1Cost;
    public Text Product2Name;
    public Text Product2Description;
    public Text Product2Cost;

    void Start()
    {
        var gameState = GameObject.Find("GameState").GetComponent<GameState>();
        spaceStation = gameState.CurrentSpaceStationData;
        player = gameState.PlayerData;
        StationName.text = spaceStation.Name;
        UpdateProduct(Product0Name, Product0Description, Product0Cost, 0);
        UpdateProduct(Product1Name, Product1Description, Product1Cost, 1);
        UpdateProduct(Product2Name, Product2Description, Product2Cost, 2);
    }

    private void UpdateProduct(Text productName, Text productDescription, Text productCost, int product)
    {
        var name = spaceStation.ProductsForSale[product].Name;
        productName.text = name;
        productDescription.text = spaceStation.ProductsForSale[product].Description;
        productCost.text = spaceStation.CurrentSellPrices[name].ToString() + " LF";
    }

    public void BuyProduct0()
    {
        BuyProduct(0);
    }

    public void BuyProduct1()
    {
        BuyProduct(1);
    }

    public void BuyProduct2()
    {
        BuyProduct(2);
    }

    public void BuyProduct(int productNum)
    {
        var price = spaceStation.CurrentSellPrices[spaceStation.ProductsForSale[productNum].Name];
        if (player.LifeForce <= price)
            ErrorText.text = "Insufficient Life Force";
        else
        {
            player.LifeForce -= price;
            player.Counts[productNum]++;
        }
    }

    public void Done()
    {
        SceneManager.LoadScene(SceneNames.Map);
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

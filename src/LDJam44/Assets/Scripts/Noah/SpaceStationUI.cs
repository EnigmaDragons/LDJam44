using Assets.Scripts.Noah;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpaceStationUI : VerboseMonoBehaviour
{
    private MutableSpaceStation spaceStation;
    private MutablePlayer player;
    private GameServices game;
    private GameState gameState;

    public AudioClip ButtonSound;
    public AudioClip BackButtonSound;
    public AudioClip BuySound;
    public Text ErrorText;
    public Text StationName;
    public GameObject MainUI;
    public GameObject UpgradesUI;
    public GameObject Tutorial;
    public Text Product0Name;
    public Text Product0Description;
    public Text Product0Cost;
    public Text Product1Name;
    public Text Product1Description;
    public Text Product1Cost;
    public Text Product2Name;
    public Text Product2Description;
    public Text Product2Cost;

    private Color errorOpaque;

    void Start()
    {
        game = VerboseFindObjectOfType<GameServices>();
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
        spaceStation = gameState.CurrentSpaceStationData;
        player = gameState.PlayerData;
        StationName.text = spaceStation.Name;
        UpdateProduct(Product0Name, Product0Description, Product0Cost, 0);
        UpdateProduct(Product1Name, Product1Description, Product1Cost, 1);
        UpdateProduct(Product2Name, Product2Description, Product2Cost, 2);

        if (gameState.SpaceStationTutorialShown)
        {
            Tutorial.SetActive(false);
            MainUI.SetActive(true);
            UpgradesUI.SetActive(false);
        }
        else
        {
            Tutorial.SetActive(true);
            MainUI.SetActive(false);
            UpgradesUI.SetActive(false);
        }
    }

    private void UpdateProduct(Text productName, Text productDescription, Text productCost, int product)
    {
        var name = spaceStation.ProductsForSale[product].Name;
        productName.text = name;
        productDescription.text = spaceStation.ProductsForSale[product].Description.Replace(". ", ". \n");
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
        game.PlaySoundEffect(BuySound);
        var price = spaceStation.CurrentSellPrices[spaceStation.ProductsForSale[productNum].Name];
        if (player.LifeForce <= price)
            StartCoroutine(ShowErrorMessage());
        else
        {
            player.LifeForce -= price;
            player.Counts[productNum]++;
        }
    }

    private IEnumerator ShowErrorMessage()
    {
        ErrorText.text = "Insufficient Life Force";
        yield return new WaitForSeconds(2.4f);
        ErrorText.text = "";
    }

    public void Done()
    {
        GameObject.Find("GameState").GetComponent<GameState>().TravelPlanData.Difficulty = (spaceStation.CurrentBuyPrices[player.Products[0].Name] * player.Counts[0]
                                                                                           + spaceStation.CurrentBuyPrices[player.Products[1].Name] * player.Counts[1]
                                                                                           + spaceStation.CurrentBuyPrices[player.Products[2].Name] * player.Counts[2]).ToString().Length;
        GameObject.FindObjectOfType<SpaceStationUndocking>().Launch();
        MainUI.SetActive(false);
    }

    public void DismissTutorial()
    {
        gameState.SpaceStationTutorialShown = true;
        Tutorial.SetActive(false);
        MainUI.SetActive(true);
    }

    public void GoToUpgrades()
    {
        game.PlaySoundEffect(ButtonSound);
        MainUI.SetActive(false);
        UpgradesUI.SetActive(true);
    }

    public void LeaveUpgrades()
    {
        game.PlaySoundEffect(BackButtonSound);
        UpgradesUI.SetActive(false);
        MainUI.SetActive(true);
    }
}

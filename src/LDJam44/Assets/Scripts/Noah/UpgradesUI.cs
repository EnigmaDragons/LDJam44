using UnityEngine;

public class UpgradesUI : MonoBehaviour
{
    private GameServices game;
    public AudioClip ButtonSound;
    public AudioClip BackButtonSound;
    public GameObject Menu;
    public GameObject Mobility;
    public GameObject Economy;
    public GameObject Combat;

    public void Start()
    {
        game = FindObjectOfType<GameServices>();
    }

    public void GoToMobility()
    {
        game.PlaySoundEffect(ButtonSound);
        Menu.SetActive(false);
        Mobility.SetActive(true);
    }

    public void GoToEconomy()
    {
        game.PlaySoundEffect(ButtonSound);
        Menu.SetActive(false);
        Economy.SetActive(true);
    }

    public void GoToCombat()
    {
        game.PlaySoundEffect(ButtonSound);
        Menu.SetActive(false);
        Combat.SetActive(true);
    }

    public void Return()
    {
        game.PlaySoundEffect(BackButtonSound);
        Mobility.SetActive(false);
        Economy.SetActive(false);
        Combat.SetActive(false);
        Menu.SetActive(true);
    }
}

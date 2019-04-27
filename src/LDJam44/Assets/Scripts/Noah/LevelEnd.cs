using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public string PlayerShipName;
    public GameObject TravelSummary;

    private Ship ship;
    private bool levelEnded = false;

    void Start()
    {
        ship = GameObject.Find(PlayerShipName).GetComponent<Ship>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (levelEnded == true)
            return;
        levelEnded = true;
        ship.Stop();
        GameObject.Instantiate(TravelSummary);
    }
}

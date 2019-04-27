using UnityEngine;

public class LevelEnd : VerboseMonoBehaviour
{
    public string PlayerShipName;
    public GameObject TravelSummary;

    private GameObject player;
    private bool levelEnded = false;

    void Start()
    {
        player = Find(PlayerShipName);
    }

    void OnTriggerEnter(Collider other)
    {
        if (levelEnded || !other.gameObject.Equals(player))
            return;
        
        levelEnded = true;
        player.GetComponent<Ship>().Stop();
        Instantiate(TravelSummary);
    }
}

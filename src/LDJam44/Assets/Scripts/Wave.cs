using System.Collections;
using UnityEngine;

public class WaveConfig
{
    public GameObject EnemyProtoype { get; set; }
    public Vector3[] Path { get; set; }
    public int NumEnemies { get; set; }
    public float SecondsBetweenEnemies { get; set; }
    public float ZTriggerThreshold {get; set;}
}

public class Wave : VerboseMonoBehaviour
{
    private WaveConfig config;
    private GameObject playerShip;

    private float zTrigger = -1;
    private bool triggered;

    public void Init(WaveConfig config)
    {
        this.config = config;
        zTrigger = config.ZTriggerThreshold;
    }

    void Start()
    {
        playerShip = Find("Spaceship");
    }

    void Update()
    {
        if (!triggered && zTrigger > -1 && playerShip.transform.position.z >= zTrigger)
        {
            triggered = true;
            StartCoroutine(Spawn());
        }
    }

    public IEnumerator Spawn()
    {
        Debug.Log($"Starting Spawning. Triggered at {zTrigger}");
        var waypoints = config.Path;
        for (var i = 0; i < config.NumEnemies; i++)
        {
            var position = waypoints[0];
            var e = Instantiate(config.EnemyProtoype, new Vector3(position.x, position.y, -3), Quaternion.identity);
            e.gameObject.SetActive(false);
            Debug.Log($"Added {waypoints.Length} waypoints for {e.name}");
            e.GetComponent<EnemyMovement>().Init(waypoints);
            e.gameObject.SetActive(true);
            yield return new WaitForSeconds(config.SecondsBetweenEnemies);
        }
        Destroy(gameObject, 0.5f);
    }
}

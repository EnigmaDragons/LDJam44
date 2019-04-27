using System.Linq;
using UnityEngine;

public class EnemyMovement : VerboseMonoBehaviour
{
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private Transform[] waypoints;

    private GameObject playerShip;
    private int nextWaypoint = 0;

    public void Init(Transform[] waypoints)
    {
        this.waypoints = waypoints;
    }
    
    void Start()
    {
        playerShip = Find("Spaceship");
        if (waypoints != null && waypoints.Any())
            gameObject.transform.position = waypoints[nextWaypoint].position;
        else
            DestroyImmediate(gameObject);
    }

    void Update()
    {
        var pos = gameObject.transform.position;
        if (nextWaypoint < waypoints.Length)
        {
            var dest = new Vector3(waypoints[nextWaypoint].position.x, waypoints[nextWaypoint].position.y, waypoints[nextWaypoint].position.z + playerShip.transform.position.z);
            gameObject.transform.position = Vector3.MoveTowards(pos, dest, speed);
            if (WithinXYEpsilon(gameObject.transform.position, dest))
                nextWaypoint++;
        }
        else
        {
            Debug.Log($"{name} arrived at final destination");
            DestroyImmediate(gameObject);
        }
    }

    bool WithinXYEpsilon(Vector3 first, Vector3 second)
    {
        const float epsilon = 0.05f;
        return Mathf.Abs(first.x - second.x) < epsilon && Mathf.Abs(first.y - second.y) < epsilon;
    }
}

using System;
using System.Linq;
using UnityEngine;

public class EnemyMovement : VerboseMonoBehaviour
{
    const float epsilon = 1.0f;

    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float rotSpeed = 2.5f;
    [SerializeField] private Transform[] editorWaypoints = new Transform[0];
    [SerializeField] private float xRotOffset;
    [SerializeField] private float yRotOffset;
    [SerializeField] private float zRotOffset;

    private Vector3[] waypoints;

    private GameObject playerShip;
    private Rigidbody rigidBody;
    private int nextWaypoint = 0;

    public void Init(Vector3[] waypoints)
    {
        this.waypoints = waypoints;
    }

    void Start()
    {
        if (waypoints == null)
            waypoints = editorWaypoints.Select(x => x.position).ToArray();

        rigidBody = VerboseGetComponent<Rigidbody>();
        playerShip = Find("Spaceship");
        if (waypoints != null && waypoints.Any())
            gameObject.transform.position = waypoints[nextWaypoint];
        else
            DestroyImmediate(gameObject);
    }

    void FixedUpdate()
    {
        var pos = gameObject.transform.position;
        if (nextWaypoint < waypoints.Length)
        {
            var dest = new Vector3(waypoints[nextWaypoint].x, waypoints[nextWaypoint].y, waypoints[nextWaypoint].z + playerShip.transform.position.z);
            ChangeVelocity(dest);
            if (WithinXYEpsilon(gameObject.transform.position, dest))
            {
                Debug.Log($"Arrived at waypoint {nextWaypoint}. Moving toward {nextWaypoint + 1}");
                nextWaypoint++;
            }
        }
        else
        {
            Debug.Log($"{name} arrived at final destination. {waypoints.Last().x}, {waypoints.Last().y}");
            DestroyImmediate(gameObject);
        }
    }

    private void ChangeVelocity(Vector3 dest)
    {
        var xDelta = dest.x - transform.position.x;
        var xVelocity = Mathf.Abs(xDelta) <= epsilon ? 0 : xDelta < 0 ? -speed : speed;
        var yDelta = dest.y - transform.position.y;
        var yVelocity = Mathf.Abs(yDelta) <= epsilon ? 0 : yDelta < 0 ? -speed : speed;
        var zDelta = dest.z - transform.position.z;
        var zVelocity = Mathf.Abs(zDelta) <= epsilon ? 0 : zDelta < 0 ? -speed : speed;
        rigidBody.velocity = new Vector3(xVelocity, yVelocity, zVelocity);
        transform.rotation = Quaternion.LookRotation(rigidBody.velocity);
    }

    bool WithinXYEpsilon(Vector3 first, Vector3 second)
    {
        return Mathf.Abs(first.x - second.x) < epsilon && Mathf.Abs(first.y - second.y) < epsilon;
    }
}

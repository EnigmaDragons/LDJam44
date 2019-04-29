using System.Linq;
using UnityEngine;

public class EnemyMovement : VerboseMonoBehaviour
{
    const float epsilon = 1.0f;

    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float rotSpeed = 2.5f;
    [SerializeField] private float flyingVariance = 6f;
    [SerializeField] private Transform[] editorWaypoints = new Transform[0];

    private Vector3[] waypoints;
    private float maxZAllowed;

    private GameObject playerShip;
    private Rigidbody rigidBody;
    private int nextWaypoint = 0;
    private Vector3 currentWaypoint;
    private bool isLeaving;

    public void Init(Vector3[] waypoints, float maxZAllowed)
    {
        this.waypoints = waypoints;
        this.maxZAllowed = maxZAllowed;
    }

    void Start()
    {
        if (waypoints == null)
        {
            waypoints = editorWaypoints.Select(x => x.position).ToArray();
            InitWaypoint(0);
        }

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
        if (!isLeaving && pos.z >= maxZAllowed)
        {
            Debug.Log($"Reached Max Z Allowed, Heading to Exit Point at increased speed");
            isLeaving = true;
            speed = speed * 2;
            nextWaypoint = waypoints.Length - 1;
        }

        if (nextWaypoint < waypoints.Length)
        {
            var dest = new Vector3(currentWaypoint.x, waypoints[nextWaypoint].y, waypoints[nextWaypoint].z + playerShip.transform.position.z);
            FaceTarget(dest);
            ChangeVelocity(dest);

            if (WithinXYEpsilon(gameObject.transform.position, dest))
            {
                Debug.Log($"Arrived at waypoint {nextWaypoint}. Moving toward {nextWaypoint + 1}");
                nextWaypoint++;
                if (nextWaypoint < waypoints.Length)
                    InitWaypoint(nextWaypoint);
            }
        }
        else
        {
            Debug.Log($"{name} arrived at final destination. {waypoints.Last().x}, {waypoints.Last().y}");
            DestroyImmediate(gameObject);
        }
    }

    private void InitWaypoint(int nextWaypoint)
    {
        currentWaypoint = waypoints[nextWaypoint] + new Vector3(
                               Random.Range(-flyingVariance, flyingVariance),
                               Random.Range(-flyingVariance, flyingVariance),
                               0);
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
    }

    void FaceTarget(Vector3 target)
    {
        var targetDir = target - transform.position;
        var step = rotSpeed * 0.4f * Time.deltaTime;
        var newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    bool WithinXYEpsilon(Vector3 first, Vector3 second)
    {
        return Mathf.Abs(first.x - second.x) < epsilon && Mathf.Abs(first.y - second.y) < epsilon;
    }
}

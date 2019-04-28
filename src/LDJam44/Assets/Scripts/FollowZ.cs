using UnityEngine;

public class FollowZ : MonoBehaviour
{
    [SerializeField] private string toFollowName = "Spaceship";

    private GameObject toFollow;
    private float zDelta;

    void Start()
    {
        toFollow = GameObject.Find(toFollowName);
        zDelta = transform.position.z;
    }

    void Update()
    {
        if (toFollow == null)
            return;

        transform.localPosition = new Vector3(transform.position.x, transform.position.y,
            toFollow.transform.position.z + zDelta);
    }
}

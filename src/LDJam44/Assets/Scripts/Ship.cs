using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] float zSpeed = 10f;
    [SerializeField] float hSpeed = 6f;
    [SerializeField] float vSpeed = 6f;
    private bool stopping = false;

    private Rigidbody Rigidbody;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!stopping)
            Rigidbody.velocity = new Vector3(hSpeed * Input.GetAxis("Horizontal"), vSpeed * Input.GetAxis("Vertical"), zSpeed);
    }

    public void Stop()
    {
        stopping = true;
        Rigidbody.velocity = new Vector3(0, 0, 0);
    }
}

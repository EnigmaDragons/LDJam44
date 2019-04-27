using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] float zSpeed = 10f;
    [SerializeField] float hSpeed = 6f;
    [SerializeField] float vSpeed = 6f;

    private Rigidbody Rigidbody;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Rigidbody.velocity = new Vector3(hSpeed * Input.GetAxis("Horizontal"), vSpeed * Input.GetAxis("Vertical"), zSpeed);
    }
}

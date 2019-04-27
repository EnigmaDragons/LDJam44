using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    private Rigidbody Rigidbody;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.velocity = transform.forward * speed;
    }

}

using System;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] float zSpeed = 10f;
    [SerializeField] float hSpeed = 8f;
    [SerializeField] float vSpeed = 8f;
    [SerializeField] Weapon weapon;
    [SerializeField] float acceleration = 0.3f;
    private bool stopping = false;
    private Rigidbody Rigidbody;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        if (Rigidbody == null || weapon == null)
            Debug.LogError("Ship is missing its Rigidbody or Weapon");
        weapon.Equip(gameObject);
    }

    void Update()
    {
        weapon.Update();
        if (Input.GetButton("Fire1"))
            weapon.Fire();
    }

    private void FixedUpdate()
    {
        if (!stopping)
            UpdateVelocity();
    }

    private void UpdateVelocity()
    {
        var currentXSpeed = Rigidbody.velocity.x;
        var targetXSpeed = hSpeed * Input.GetAxis("Horizontal");
        var newXSpeed = Mathf.Clamp(targetXSpeed, currentXSpeed - acceleration, currentXSpeed + acceleration);

        var currentYSpeed = Rigidbody.velocity.y;
        var targetYSpeed = vSpeed * Input.GetAxis("Vertical");
        var newYSpeed = Mathf.Clamp(targetYSpeed, currentYSpeed - acceleration, currentYSpeed + acceleration);

        Rigidbody.velocity = new Vector3(newXSpeed, newYSpeed, zSpeed);
    }

    public void Stop()
    {
        stopping = true;
        Rigidbody.velocity = new Vector3(0, 0, 0);
    }
}

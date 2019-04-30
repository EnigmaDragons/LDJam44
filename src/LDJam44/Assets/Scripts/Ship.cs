using UnityEngine;

public class Ship : VerboseMonoBehaviour
{
    [SerializeField] float zSpeed = 10f;
    [SerializeField] float hSpeed = 8f;
    [SerializeField] float vSpeed = 8f;
    [SerializeField] Weapon weapon;
    [SerializeField] Targeting targetingSystem;
    [SerializeField] float acceleration = 0.3f;

    private bool stopping = false;
    private Rigidbody Rigidbody;
    private Health health;

    public bool Stopped => stopping;

    void Start()
    {
        health = VerboseGetComponent<Health>();
        Rigidbody = VerboseGetComponent<Rigidbody>();
        if (weapon == null)
            Debug.LogError("Ship is missing its Weapon");

        var gameState = GameObject.Find("GameState").GetComponent<GameState>();
        zSpeed = gameState.UpgradeEffect("Engines");
        hSpeed = gameState.UpgradeEffect("Thrusters");
        vSpeed = gameState.UpgradeEffect("Thrusters");
        acceleration = gameState.UpgradeEffect("Stabilizers");
        weapon.Equip(gameObject, gameState.UpgradeEffect("Damage"));
        health.SetDamageReduction(gameState.UpgradeEffect("Armor"));
    }

    void Update()
    {
        weapon.Update();
        if (Input.GetButton("Fire1") && !stopping)
            weapon.FireHoming(targetingSystem.Target?.gameObject);
    }

    private void FixedUpdate()
    {
        if (!stopping)
        {
            UpdateVelocity();
            UpdateLeaning();
            ClampShip();
        }
    }

    private void UpdateLeaning()
    {
        var vLeanAmount = Rigidbody.velocity.x * 5;
        var vTurnAmount = Rigidbody.velocity.x * 2;
        var hLeanAmount = Rigidbody.velocity.y * 3;
        transform.rotation = Quaternion.Euler(-hLeanAmount, vTurnAmount, 180 - vLeanAmount);
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

    private void ClampShip()
    {
        var clampedX = Mathf.Clamp(transform.position.x, SpawnBoundaries.minScreenX, SpawnBoundaries.maxScreenX);
        var clampedY = Mathf.Clamp(transform.position.y, SpawnBoundaries.minScreenY + SpawnBoundaries.yOffset, SpawnBoundaries.maxScreenY + SpawnBoundaries.yOffset);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    public void Stop()
    {
        stopping = true;
        Rigidbody.velocity = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(0, 0, 180);
        health.ActivateInvincible();
    }
}

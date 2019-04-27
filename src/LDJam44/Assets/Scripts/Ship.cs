using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] float zSpeed = 10f;
    [SerializeField] float hSpeed = 6f;
    [SerializeField] float vSpeed = 6f;
    [SerializeField] Weapon weapon;
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
        if (!stopping)
            Rigidbody.velocity = new Vector3(hSpeed * Input.GetAxis("Horizontal"), vSpeed * Input.GetAxis("Vertical"), zSpeed);
        if (Input.GetButton("Fire1"))
            weapon.Fire();
    }

    public void Stop()
    {
        stopping = true;
        Rigidbody.velocity = new Vector3(0, 0, 0);
    }
}

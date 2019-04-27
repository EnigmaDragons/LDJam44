using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    [SerializeField] int damageAmount = 10;
    [SerializeField] Role appliesTo = Role.All; 

    bool resolvedDamage;

    private void OnCollisionEnter(Collision collision) => ApplyDamageTo(collision.gameObject);
    private void OnTriggerEnter(Collider other) => ApplyDamageTo(other.gameObject);

    private void ApplyDamageTo(GameObject target)
    {
        var health = target.GetComponent<Health>();
        if (health == null)
            Debug.LogError($"No Health Component Found on {target.name}");
        health?.ApplyDamage(damageAmount);
    }
}

using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    [SerializeField] int damageAmount = 10;
    [SerializeField] Role appliesTo = Role.All;
    [SerializeField] AudioClip soundEffect;

    bool resolvedDamage;

    private void OnCollisionEnter(Collision collision) => ApplyDamageTo(collision.gameObject);
    private void OnTriggerEnter(Collider other) => ApplyDamageTo(other.gameObject);

    private void ApplyDamageTo(GameObject target)
    {
        if (resolvedDamage)
            return;

        var health = target.GetComponent<Health>();
        if (health == null)
            Debug.LogError($"No Health Component Found on {target.name}");
        else
        {
            health?.ApplyDamage(damageAmount);
            AudioSource.PlayClipAtPoint(soundEffect, Camera.main.transform.position);
            resolvedDamage = true;
        }
    }
}

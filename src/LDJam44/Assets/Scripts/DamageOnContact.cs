using UnityEngine;

public class DamageOnContact : VerboseMonoBehaviour
{
    [SerializeField] int damageAmount = 10;
    [SerializeField] Role appliesTo = Role.All;
    [SerializeField] AudioClip soundEffect;

    private GameServices game;
    private bool resolvedDamage;

    private void OnCollisionEnter(Collision collision) => ApplyDamageTo(collision.gameObject);
    private void OnTriggerEnter(Collider other) => ApplyDamageTo(other.gameObject);

    private void Start()
    {
        game = VerboseFindObjectOfType<GameServices>();   
    }

    private void ApplyDamageTo(GameObject target)
    {
        if (resolvedDamage)
            return;

        Debug.Log($"{target.name} was hit by {name}");
        var health = target.GetComponent<Health>();
        if (health == null)
            Debug.Log($"No Health Component Found on {target.name}");
        else
        {
            health?.ApplyDamage(damageAmount);
            game.PlaySoundEffect(soundEffect);
            resolvedDamage = true;
        }
    }
}

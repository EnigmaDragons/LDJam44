using Assets.Scripts;
using System.Collections;
using UnityEngine;

public class Health : VerboseMonoBehaviour
{
    [SerializeField] Role role;
    [SerializeField] int maxHp;
    [SerializeField] int currentHp;
    [SerializeField] AudioClip onDeath;
    [SerializeField] GameObject onDeathVfx;

    private bool destructionStarted;
    private SimpleGameEvents gameEvents;

    public void Start()
    {
        gameEvents = VerboseFindObjectOfType<SimpleGameEvents>();
    }

    public void ApplyDamage(int amount)
    {
        if (destructionStarted)
            return;

        currentHp -= amount;
        ProcessDestruction();
    }

    private void ProcessDestruction()
    {
        if (currentHp <= 0)
        {
            destructionStarted = true;
            Debug.Log($"{name} is destroyed");
            PlayExplosion();
            StartCoroutine(ResolveDestruction());
        }
    }

    private void PlayExplosion()
    {
        var explosion = Instantiate(onDeathVfx, transform.position, transform.rotation);
        var explosionRigidBody = explosion.GetComponent<Rigidbody>();
        var rigidBody = GetComponent<Rigidbody>();
        if (rigidBody != null && explosionRigidBody != null)
            explosionRigidBody.velocity = rigidBody.velocity;
        AudioSource.PlayClipAtPoint(onDeath, transform.position);
    }

    private IEnumerator ResolveDestruction()
    {
        yield return new WaitForSeconds(0.3f);
        if (role.Equals(Role.Player))
            gameEvents.OnGameOver();
        Destroy(gameObject);
    }
}

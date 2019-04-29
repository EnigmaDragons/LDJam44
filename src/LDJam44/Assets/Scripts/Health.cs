using System;
using System.Collections;
using UnityEngine;

public class Health : VerboseMonoBehaviour
{
    public Role role;
    public int maxHp;
    public int currentHp;
    public int loot = 0;
    [SerializeField] AudioClip onDeath;
    [SerializeField] AudioClip onDamageSound;
    [SerializeField] GameObject onDeathVfx;

    public float HpPercent => (float)currentHp / (float)maxHp;

    private int damageReductionAmount = 0;
    private bool isInvincible;
    private bool destructionStarted;
    private GameServices game;
    private GameState state;
    private Action onDamage = () => {};

    public void Init(int maxHp)
    {
        this.maxHp = maxHp;
        currentHp = maxHp;
    }

    public void ActivateInvincible() => isInvincible = true;
    public void DeactivateInvincible() => isInvincible = false;

    public void OnDamage(Action a)
    {
        onDamage = a;
    }

    public void Start()
    {
        game = VerboseFindObjectOfType<GameServices>();
        state = GameObject.Find("GameState").GetComponent<GameState>();
        currentHp = maxHp;
    }

    public void ApplyDamage(int amount)
    {
        if (destructionStarted || isInvincible)
            return;

        if (onDamageSound != null)
            game.PlaySoundEffect(onDamageSound);
        currentHp -= amount - damageReductionAmount;
        ProcessDestruction();
        onDamage();
    }

    public void SetDamageReduction(float v)
    {
        damageReductionAmount = Convert.ToInt32(v);
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
        explosion.transform.localScale = GetComponent<Renderer>().bounds.size;
        var explosionRigidBody = explosion.GetComponent<Rigidbody>();
        var rigidBody = GetComponent<Rigidbody>();
        if (rigidBody != null && explosionRigidBody != null)
            explosionRigidBody.velocity = rigidBody.velocity;

        game.PlaySoundEffect(onDeath);
    }

    private IEnumerator ResolveDestruction()
    {
        yield return new WaitForSeconds(0.3f);
        if (role.Equals(Role.Player))
            game.OnGameOver();
        if (role.Equals(Role.Enemy))
            state.PlayerData.Loot += (int)Math.Ceiling(loot * state.UpgradeEffect("Looting"));
        Destroy(gameObject);
    }
}

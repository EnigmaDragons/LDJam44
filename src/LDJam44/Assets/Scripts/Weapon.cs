using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponName", menuName = "Weapon", order = 1)]
class Weapon : ScriptableObject
{
    [SerializeField] GameObject projectilePrototype;
    [SerializeField] float fireInterval = 0.6f;
    [SerializeField] AudioClip fireSound;
    [SerializeField] float forwardOffset = 2f;
    [SerializeField] float numProjectiles = 1;
    [SerializeField] float delayBetweenShots = 0.18f;
    [SerializeField] Role weaponRole = Role.Enemy;
    [SerializeField] float accuracy = 1.0f;
    [SerializeField] int damageAmount = 10;

    private float damageFactor = 1.0f;
    private double msBeforeFire;
    private GameObject owner;
    private GameServices game;
    
    public void Equip(GameObject owner, float damageFactor = 1.0f)
    {
        this.owner = owner;
        this.damageFactor = damageFactor;
        game = FindObjectOfType<GameServices>();
    }

    public void Update()
    {
        if (msBeforeFire > 0)
            msBeforeFire -= Time.deltaTime;
    }

    public void FireTowards(Vector3 target)
    {
        var targetDir = target - owner.transform.position;
        var newDir = Vector3.RotateTowards(owner.transform.forward, targetDir, float.MaxValue, 0.0f);
        var targetRotation = Quaternion.LookRotation(newDir);

        Fire(targetRotation);
    }

    public void Fire()
    {
        Fire(owner.transform.rotation);
    }

    public void FireHoming(GameObject target)
    {
        Fire(owner.transform.rotation, target);
    }

    private void Fire(Quaternion rotation, GameObject target = null)
    {
        if (msBeforeFire > 0)
            return;

        msBeforeFire = fireInterval;

        game.StartInBackround(LaunchProjectiles(rotation, target));
    }

    private IEnumerator LaunchProjectiles(Quaternion rotation, GameObject target)
    {
        for (var i = 0; i < numProjectiles; i++)
        {
            var ownerPos = owner.transform.position;
            var ownerDirection = owner.transform.forward;
            var spawnPos = ownerPos + ownerDirection * forwardOffset;

            game.PlaySoundEffect(fireSound);
            var p = Instantiate(projectilePrototype, spawnPos, rotation);
            var projectile = p.GetComponent<ParticleCollisionInstance>();
            projectile.SetDamage(damageAmount);
            projectile.AmplifyDamage(damageFactor);
            projectile.SetRole(weaponRole);
            projectile.SetHomingTarget(target);
            yield return new WaitForSeconds(delayBetweenShots);
        }
    }
}

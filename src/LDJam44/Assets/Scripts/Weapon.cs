using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponName", menuName = "Weapon", order = 1)]
class Weapon : ScriptableObject
{
    [SerializeField] GameObject projectile;
    [SerializeField] float fireInterval = 0.6f;
    [SerializeField] AudioClip fireSound;
    [SerializeField] float forwardOffset = 2f;
    [SerializeField] float numProjectiles = 1;
    [SerializeField] float delayBetweenShots = 0.18f;
    [SerializeField] Role weaponRole = Role.Enemy;
    [SerializeField] float accuracy = 1.0f;

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

    private void Fire(Quaternion rotation)
    {
        if (msBeforeFire > 0)
            return;

        msBeforeFire = fireInterval;

        game.StartInBackround(LaunchProjectiles(rotation));
    }

    private IEnumerator LaunchProjectiles(Quaternion rotation)
    {
        for (var i = 0; i < numProjectiles; i++)
        {
            const float accuracyFactor = 0.6f;
            var inaccuracyX = Random.Range(-((1f - accuracy) * accuracyFactor), ((1f - accuracy) * accuracyFactor));
            var inaccuracyY = Random.Range(-((1f - accuracy) * accuracyFactor), ((1f - accuracy) * accuracyFactor));
            var inaccuracyZ = Random.Range(-((1f - accuracy) * accuracyFactor), ((1f - accuracy) * accuracyFactor));

            var shotRotation = rotation * Quaternion.LookRotation(new Vector3(inaccuracyX, inaccuracyY, inaccuracyZ));

            var ownerPos = owner.transform.position;
            var ownerDirection = owner.transform.forward;
            var spawnPos = ownerPos + ownerDirection * forwardOffset;

            game.PlaySoundEffect(fireSound);
            var p = Instantiate(projectile, spawnPos, rotation);
            p.GetComponent<ParticleCollisionInstance>().AmplifyDamage(damageFactor);
            p.GetComponent<ParticleCollisionInstance>().SetRole(weaponRole);
            yield return new WaitForSeconds(delayBetweenShots);
        }
    }
}

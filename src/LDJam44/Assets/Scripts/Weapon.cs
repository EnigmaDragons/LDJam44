using UnityEngine;

[CreateAssetMenu(fileName = "WeaponName", menuName = "Weapon", order = 1)]
class Weapon : ScriptableObject
{
    [SerializeField] GameObject projectile;
    [SerializeField] float fireInterval = 0.6f;
    [SerializeField] AudioClip fireSound;
    [SerializeField] float forwardOffset = 2f;

    private double msBeforeFire;
    private GameObject owner;
    private GameServices game;
    
    public void Equip(GameObject owner)
    {
        this.owner = owner;
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
        game.PlaySoundEffect(fireSound);

        var ownerPos = owner.transform.position;
        var ownerDirection = owner.transform.forward;
        var spawnPos = ownerPos + ownerDirection * forwardOffset;

        Instantiate(projectile, spawnPos, rotation);
    }
}

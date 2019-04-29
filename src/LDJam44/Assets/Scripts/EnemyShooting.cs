using UnityEngine;

public class EnemyShooting : VerboseMonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private float varianceAmount = 2.5f;

    private Weapon cachedWeapon;
    private Ship target;
    
    private void Start()
    {
        target = VerboseFindObjectOfType<Ship>();
        cachedWeapon = Instantiate(weapon, gameObject.transform);
        cachedWeapon.Equip(gameObject);
    }

    private void Update()
    {
        cachedWeapon.Update();
        if (target != null && SpawnBoundaries.IsInEnemyPlayArea(transform.position, target.transform.position))
            cachedWeapon.FireTowards(target.transform.position + VarianceAmount());
    }

    private Vector3 VarianceAmount() => new Vector3(Random.Range(-varianceAmount, varianceAmount), Random.Range(-varianceAmount, varianceAmount), 0);
}

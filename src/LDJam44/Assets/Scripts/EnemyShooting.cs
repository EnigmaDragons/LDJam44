using UnityEngine;

public class EnemyShooting : VerboseMonoBehaviour
{
    [SerializeField] private Weapon weapon;

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
        if (SpawnBoundaries.IsInEnemyPlayArea(transform.position, target.transform.position))
            cachedWeapon.FireTowards(target.transform.position);
    }
}

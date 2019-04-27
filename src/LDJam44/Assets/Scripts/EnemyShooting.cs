using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private Weapon weapon;

    private Weapon cachedWeapon;
    
    private void Start()
    {
        cachedWeapon = Instantiate(weapon, gameObject.transform);
        cachedWeapon.Equip(gameObject);
    }

    private void Update()
    {
        cachedWeapon.Update();
        cachedWeapon.Fire();
    }
}

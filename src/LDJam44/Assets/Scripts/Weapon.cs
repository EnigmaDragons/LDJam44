using UnityEngine;

[CreateAssetMenu(fileName = "WeaponName", menuName = "Weapon", order = 1)]
class Weapon : ScriptableObject
{
    [SerializeField] GameObject projectile;
    [SerializeField] float fireInterval = 0.6f;
    [SerializeField] AudioClip fireSound;

    private double msBeforeFire;
    private GameObject owner;

    public void Equip(GameObject owner)
    {
        this.owner = owner;
    }

    public void Update()
    {
        if (msBeforeFire > 0)
            msBeforeFire -= Time.deltaTime;
    }

    public void Fire()
    {
        if (msBeforeFire > 0)
            return;

        msBeforeFire = fireInterval;
        Instantiate(projectile, new Vector3(owner.transform.position.x, owner.transform.position.y, owner.transform.position.z + 2f), owner.transform.rotation);
        AudioSource.PlayClipAtPoint(fireSound, Camera.main.transform.position);
    }
}

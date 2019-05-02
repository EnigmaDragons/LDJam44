using UnityEngine;

public class EnemyShooting : VerboseMonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private float varianceAmount = 2.5f;
    [SerializeField] private float difficultyDamageScaling = 1.25f;
    [SerializeField] private float distanceMultiplier = 1f;
    [SerializeField] private float engineMultiplier = 1f;

    private GameState gameState;
    private Weapon cachedWeapon;
    private Ship target;
    
    private void Start()
    {
        target = VerboseFindObjectOfType<Ship>();
        cachedWeapon = Instantiate(weapon, gameObject.transform);
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
        var damageFactor = difficultyDamageScaling * gameState.TravelPlanData.Difficulty;
        cachedWeapon.Equip(gameObject, damageFactor);
    }

    private void Update()
    {
        cachedWeapon.Update();
        if (target != null && SpawnBoundaries.IsInEnemyPlayArea(transform.position, target.transform.position))
        {
            float zOffset = 0;
            zOffset += Vector3.Distance(target.transform.position, transform.position) * distanceMultiplier;
            zOffset += gameState.UpgradeEffect("Engines") * engineMultiplier;

            var targetedSpot = new Vector3(target.transform.position.x, target.transform.position.y + 0.01f, target.transform.position.z + zOffset);
            cachedWeapon.FireTowards(targetedSpot + VarianceAmount());
        }
    }

    private Vector3 VarianceAmount() => new Vector3(Random.Range(-varianceAmount, varianceAmount), Random.Range(-varianceAmount, varianceAmount), 0);
}

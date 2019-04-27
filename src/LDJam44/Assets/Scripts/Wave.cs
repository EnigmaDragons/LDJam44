using System.Collections;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave")]
public class Wave : ScriptableObject
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject path;
    [SerializeField] private float spawnInterval = 1.0f;
    [SerializeField] private int numEnemies = 3;

    private Transform[] Waypoints => path.transform.OfType<Transform>().Select(x => x).ToArray();

    public IEnumerator Spawn()
    {
        var waypoints = Waypoints;
        for (var i = 0; i < numEnemies; i++)
        {
            var position = waypoints[0].transform.position;
            var e = Instantiate(enemy, new Vector3(position.x, position.y, -3), Quaternion.identity);
            e.gameObject.SetActive(false);
            Debug.Log($"Added {waypoints.Length} waypoints for {e.name}");
            e.GetComponent<EnemyMovement>().Init(waypoints);
            e.gameObject.SetActive(true);
            yield return new WaitForSeconds(Random.Range(spawnInterval, spawnInterval));
        }
    }
}

using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    [SerializeField] private float numSecondsBetweenWaves = 2f;

    private int startingWave = 0;
    private int currentWave = 0;

    public void Start()
    {
        StartCoroutine(Spawn(startingWave));
    }

    private void OnDestroy()
    {
        Event.Unsubscribe(this);
    }
    
    private IEnumerator Spawn(int index)
    {
        while (true)
        {
            yield return StartCoroutine(waves[index].Spawn());
            currentWave++;
            index = currentWave % waves.Length;
            yield return new WaitForSeconds(numSecondsBetweenWaves);
        }
    }
}

using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // Settings
    [SerializeField] float travelDistance = 95f;
    [SerializeField] int difficulty = 3;

    // Object Prototypes
    [SerializeField] GameObject levelEnd;
    [SerializeField] GameObject spaceStation;

    void Start()
    {
        Instantiate(levelEnd, new Vector3(0, 0, travelDistance), Quaternion.identity);
        Instantiate(spaceStation, new Vector3(0, 0, travelDistance + 5f), Quaternion.identity);
    }

    void Update()
    {
        
    }
}

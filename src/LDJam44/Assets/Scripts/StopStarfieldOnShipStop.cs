using UnityEngine;

class StopStarfieldOnShipStop : VerboseMonoBehaviour
{
    private ParticleSystem particles;
    private Ship ship;

    private void Start()
    {
        ship = VerboseFindObjectOfType<Ship>();
        particles = VerboseGetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (ship.Stopped)
            Destroy(gameObject);
    }
}

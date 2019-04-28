using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) => Obliterate(collision.gameObject);
    private void OnTriggerEnter(Collider other) => Obliterate(other.gameObject);
    private void OnParticleCollision(GameObject other) => Obliterate(other);
    private void Obliterate(GameObject other) => Destroy(other);
}

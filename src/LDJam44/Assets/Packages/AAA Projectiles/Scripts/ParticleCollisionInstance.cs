/*This script created by using docs.unity3d.com/ScriptReference/MonoBehaviour.OnParticleCollision.html*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleCollisionInstance : MonoBehaviour
{
    public GameObject[] EffectsOnCollision;
    public float Offset = 0;
    public float DestroyTimeDelay = 5;
    public int Damage = 10;
    public AudioClip HitSoundEffect;
    public bool UseWorldSpacePosition;
    public bool UseFirePointRotation;
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    private ParticleSystem ps;

    private float zStart;
    private bool isDestroying;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        Destroy(gameObject, 30f);
    }

    void Update()
    {
        Debug.Log($"Shot Z {transform.position.z}");
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log($"Particle Collided with {other.name}");
        var health = other.GetComponent<Health>();
        health?.ApplyDamage(Damage);
        AudioSource.PlayClipAtPoint(HitSoundEffect, Camera.main.transform.position);

        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);     
        for (int i = 0; i < numCollisionEvents; i++)
        {
            foreach (var effect in EffectsOnCollision)
            {
                var instance = Instantiate(effect, collisionEvents[i].intersection + collisionEvents[i].normal * Offset, new Quaternion()) as GameObject;
                if (UseFirePointRotation) { instance.transform.LookAt(transform.position); }
                else { instance.transform.LookAt(collisionEvents[i].intersection + collisionEvents[i].normal); }
                if (!UseWorldSpacePosition) instance.transform.parent = transform;
                Destroy(instance, DestroyTimeDelay);
            }
        }
        Destroy();
    }

    private void Destroy()
    {
        if (isDestroying)
            return;

        isDestroying = true;
        Destroy(gameObject, DestroyTimeDelay + 0.5f);
    }
}

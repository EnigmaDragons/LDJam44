/*This script created by using docs.unity3d.com/ScriptReference/MonoBehaviour.OnParticleCollision.html*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ParticleCollisionInstance : VerboseMonoBehaviour
{
    public GameObject[] EffectsOnCollision;
    public float Offset = 0;
    public float DestroyTimeDelay = 5;
    public int Damage = 10;
    public AudioClip HitSoundEffect;
    public bool UseWorldSpacePosition;
    public bool UseFirePointRotation;
    private ParticleSystem part;
    private GameServices game;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private float zStart;
    private bool isDestroying;
    private float damageFactor = 1.0f;
    private Role ownedBy = Role.All;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        game = FindObjectOfType<GameServices>();
        Destroy(gameObject, 30f);
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log($"Particle Collided with {other.name}");
        var health = other.GetComponent<Health>();
        if (health != null && health.role.Equals(ownedBy))
            return;

        health?.ApplyDamage(Convert.ToInt32(Damage * damageFactor));
        game.PlaySoundEffect(HitSoundEffect);

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

    public void SetDamage(int amount)
    {
        Damage = amount;
    }

    public void AmplifyDamage(float damageFactor)
    {
        this.damageFactor = damageFactor;
    }

    public void SetRole(Role weaponRole)
    {
        ownedBy = weaponRole;
    }

    private void Destroy()
    {
        if (isDestroying)
            return;

        isDestroying = true;
        Destroy(gameObject, DestroyTimeDelay + 0.5f);
    }

    //Homing System
    private GameObject target;
    private ParticleSystem.Particle[] particles;

    public void SetHomingTarget(GameObject enemy)
    {
        Debug.Log("Homing Projectile Fired");
        target = enemy;
    }

    void Update()
    {
        if (target == null)
            return;
        if (particles == null)
            particles = new ParticleSystem.Particle[part.main.maxParticles];
        var particleCount = part.GetParticles(particles);
        var heading = target.transform.position - particles[0].position;
        var distance = heading.magnitude;
        var direction = heading / distance;
        for (int i = 0; i < particleCount; i++)
            particles[i].velocity = direction * particles[i].velocity.magnitude;
        part.SetParticles(particles);
    }
}

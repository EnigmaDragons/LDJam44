﻿using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    public float speed = 15f;
    public float hitOffset = 0f;
    public float maxTravelDistance = 80;
    public bool UseFirePointRotation;
    public GameObject hit;
    public GameObject flash;

    private float zStart;

    void Start ()
    {
        zStart = transform.position.z;
        if (flash != null)
        {
            var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if (flashPs == null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
        }
	}

    void FixedUpdate ()
    {
		if (speed != 0)
            transform.position += transform.forward * (speed * Time.deltaTime);   

        if (transform.position.z - zStart >= maxTravelDistance)
            DestroyImmediate(gameObject);
    }

    //https ://docs.unity3d.com/ScriptReference/Rigidbody.OnCollisionEnter.html
    void OnCollisionEnter(Collision collision)
    {
        speed = 0;   
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point + contact.normal * hitOffset;

        if (hit != null)
        {
            var hitInstance = Instantiate(hit, pos, rot);
            if (UseFirePointRotation)
            {
                var objRot = gameObject.transform.rotation;
                var factor = 1.0f;
                //var factor = 0.3f;
                hitInstance.transform.rotation = Quaternion.Euler(objRot.x * factor, objRot.y * factor, objRot.z * factor) * Quaternion.Euler(0, 180f, 0);
            }
            else
            { hitInstance.transform.LookAt(contact.point + contact.normal); }

            var hitPs = hitInstance.GetComponent<ParticleSystem>();
            if (hitPs == null)
            {
                Destroy(hitInstance, hitPs.main.duration);
            }
            else
            {
                var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitInstance, hitPsParts.main.duration);
            }
        }
        Destroy(gameObject);
    }
}

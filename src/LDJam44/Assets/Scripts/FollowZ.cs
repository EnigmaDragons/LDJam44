﻿using UnityEngine;

public class FollowZ : MonoBehaviour
{
    [SerializeField] private string toFollowName;

    private GameObject toFollow;
    private float zDelta;

    void Start()
    {
        toFollow = GameObject.Find(toFollowName);
        zDelta = transform.position.z;
    }

    void Update()
    {
        transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, toFollow.transform.position.z + zDelta), transform.rotation);
    }
}
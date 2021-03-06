﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotating : MonoBehaviour
{
    public Rotating Rotating;
    public int MaxRotation;

    void Start()
    {
        Rotating.ZSpeed = Random.Range(-MaxRotation, MaxRotation + 1);
        Rotating.XSpeed = Random.Range(-MaxRotation, MaxRotation + 1);
        Rotating.YSpeed = Random.Range(-MaxRotation, MaxRotation + 1);
    }
}

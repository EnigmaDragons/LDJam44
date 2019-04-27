using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public virtual void Attack(Vector3 target) { }
    public virtual void Move(Vector3 location,float moveSpeed) { }
}



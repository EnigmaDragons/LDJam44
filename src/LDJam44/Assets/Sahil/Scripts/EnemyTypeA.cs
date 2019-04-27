using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeA : Enemy
{
    public float Speed;
    public GameObject target;
    private Vector3 targetLocation;
    public float moveToRadius = 10f;


    public float Accuracy = 33.33f;
    int fireCount;

    public override void Attack(Vector3 target)
    {
        int diceRoll = Random.Range(0, 100);
        Vector3 targetLocation = (float)diceRoll <= Accuracy ? target : target + new Vector3(Random.Range(-3,3), Random.Range(-3, 3), Random.Range(-3, 3));
    }

    public override void Move(Vector3 location,float moveSpeed)
    {
        if ((location - transform.position).magnitude >= moveToRadius)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, location, moveSpeed * Time.deltaTime);
        }
    }
    public void AddMovementVector(Vector3 input)
    {
        targetLocation += input; 
    }

    void Update()
    {
        Move(new Vector3(target.transform.position.x,target.transform.position.y,0), Speed);
        

    }
}

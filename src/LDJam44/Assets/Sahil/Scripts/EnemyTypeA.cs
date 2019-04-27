using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeA : Enemy
{
    public float Speed;
    public GameObject target;
    public float Accuracy = 33.33f;
    int fireCount;
    public override void Attack(Vector3 target)
    {
        int diceRoll = Random.Range(0, 100);
        Vector3 targetLocation = (float)diceRoll <= Accuracy ? target : target + new Vector3(Random.Range(-3,3), Random.Range(-3, 3), Random.Range(-3, 3));

        

     
        if((float)diceRoll <= Accuracy)
        {
            targetLocation = target;
        }
        else
        {
            
        }
    }

    public override void Move(Vector3 location,float moveSpeed)
    {
        this.transform.position = Vector3.Lerp(this.transform.position, location, moveSpeed * Time.deltaTime);
    }

    void Update()
    {
        Move(target.transform.position, Speed);
    }
}

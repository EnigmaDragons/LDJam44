using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButtonRotation : MonoBehaviour
{
    bool x = false;

    void OnMouseEnter()
    {
        x = true;
    }
    void OnMouseExit()
    {
        x = false;
    }
    void Update()
    {
        if (x)
        {
            transform.Rotate(0, 0, 1);
        }
    }
}

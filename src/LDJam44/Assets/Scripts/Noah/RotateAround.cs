using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public Vector3 OrbitLocation;
    public int Speed;

    void Update()
    {
        transform.RotateAround(OrbitLocation, Vector3.down, Speed * Time.deltaTime);
    }
}

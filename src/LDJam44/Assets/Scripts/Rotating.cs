using UnityEngine;

public class Rotating : MonoBehaviour
{
    public float XSpeed;
    public float YSpeed;
    public float ZSpeed;

    void Update()
    {
        this.transform.Rotate(Time.deltaTime * XSpeed, Time.fixedDeltaTime * YSpeed, Time.deltaTime * ZSpeed);
    }
}

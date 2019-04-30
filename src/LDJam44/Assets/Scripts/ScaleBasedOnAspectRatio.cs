using UnityEngine;

public class ScaleBasedOnAspectRatio : MonoBehaviour
{
    public Camera Camera;

    public void Start()
    {
        var aspectRatio = Camera.aspect;
        Camera.transform.position = new Vector3(Camera.transform.position.x, 21.334f / aspectRatio, Camera.transform.position.z);
    }
}

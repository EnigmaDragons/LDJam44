using UnityEngine;

class CameraSmoothFollow : VerboseMonoBehaviour
{
    public float maxX = 6f;
    public float minX = -6f;
    public float maxY = 4f;
    public float minY = -4f;
    public float cameraSpeed = 0.2f;

    private Ship ship;
    private Vector3 cameraTarget;
    private float yOffset;
    
    private void Start()
    {
        yOffset = transform.position.y;
        ship = VerboseFindObjectOfType<Ship>();
    }

    private void FixedUpdate()
    {
        var newX = Mathf.Clamp(ship.transform.position.x * 0.6f, minX, maxX);
        var newY = Mathf.Clamp((ship.transform.position.y + yOffset) * 0.6f, minY, maxY);
        cameraTarget = new Vector3(newX, newY, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, cameraTarget, cameraSpeed);
    }
}

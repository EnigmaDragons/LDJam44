using System.Collections;
using UnityEngine;

public class FlyToStation : MonoBehaviour
{
    public float ForwardSpeed;
    public float RotationSpeed;

    private GameServices game;
    private Vector3 travelLocation;
    public bool IsTraveling { get; private set; }

    void Start()
    {
        game = FindObjectOfType<GameServices>();
        transform.LookAt(new Vector3(0, 1, 0));
    }

    void Update()
    {
        if (!IsTraveling)
            return;

        var targetDir = travelLocation - transform.position;
        var rotationStep = RotationSpeed * Time.deltaTime;
        var newDir = Vector3.RotateTowards(transform.forward, targetDir, rotationStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        var step = Time.deltaTime * ForwardSpeed;
        transform.position = Vector3.MoveTowards(transform.position, travelLocation, step);
    }

    public void TravelTo(Vector3 location)
    {
        if (IsTraveling)
            return;
        IsTraveling = true;
        travelLocation = location;
        StartCoroutine(SceneChange());
    }

    public IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(1);
        game.NavigateToScene(SceneNames.ShipTravel);
    }
}

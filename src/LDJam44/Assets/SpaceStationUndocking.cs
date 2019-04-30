using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class SpaceStationUndocking : MonoBehaviour
{
    public AudioClip EnginesOn;
    public AudioClip Boost;

    private GameServices game;
    private bool isTraveling;
    private Vector3 startPositiion;
    private Vector3 startRotation;
    private float timeSpent;
    private float timeToReachTarget;
    private Vector3 endPosition;
    private Vector3 endRotation;

    void Start()
    {
        game = FindObjectOfType<GameServices>();
    }

    void Update()
    {
        if (!isTraveling)
            return;
        timeSpent += Time.deltaTime / timeToReachTarget;
        transform.position = Vector3.Lerp(startPositiion, endPosition, timeSpent);
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(startRotation), Quaternion.Euler(endRotation), timeSpent);
    }

    public void Launch()
    {
        StartCoroutine(Undock());
    }

    private IEnumerator Undock()
    {
        isTraveling = true;
        game.PlaySoundEffect(EnginesOn);
        GoTo(new Vector3(0f, 1.5f, -0.5f), new Vector3(0, -90, 180), 1);
        yield return new WaitForSeconds(1.5f);
        game.PlaySoundEffect(Boost);
        GoTo(new Vector3(-20f, 1.5f, -0.5f), new Vector3(0, -90, 180), 2.5f);
        yield return new WaitForSeconds(2.5f);
        game.NavigateToScene(SceneNames.Map);
    }

    private void GoTo(Vector3 position, Vector3 rotation, float seconds)
    {
        startPositiion = transform.position;
        startRotation = transform.rotation.eulerAngles;
        timeSpent = 0;
        timeToReachTarget = seconds;
        endPosition = position;
        endRotation = rotation;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatAnimation : MonoBehaviour
{
    private GameServices game;
    public AudioClip ExplosionSound;
    public GameObject Spaceship;
    public GameObject Explosion;
    public GameObject Debris;

    void Start()
    {
        game = FindObjectOfType<GameServices>();
        StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        yield return new WaitForSeconds(2);
        Explosion.SetActive(true);
        game.PlaySoundEffect(ExplosionSound);
        yield return new WaitForSeconds(0.1f);
        Spaceship.SetActive(false);
        Debris.SetActive(true);
    }
}

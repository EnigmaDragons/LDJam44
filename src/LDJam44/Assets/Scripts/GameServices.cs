using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameServices : MonoBehaviour
{
    public AudioSource SFX;

    public void OnGameOver()
    {
        StartCoroutine(TriggerGameOver());
    }

    private IEnumerator TriggerGameOver()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(SceneNames.LoseScene);
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }
}

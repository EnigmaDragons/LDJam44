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
        if (clip != null)
            SFX.PlayOneShot(clip);
    }

    public void NavigateToScene(string sceneName) => StartCoroutine(NavigateAfterDelay(sceneName));
    private IEnumerator NavigateAfterDelay(string sceneName)
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(sceneName);
    }

    public void StartInBackround(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}

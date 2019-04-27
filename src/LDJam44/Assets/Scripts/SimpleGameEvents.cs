using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class SimpleGameEvents : MonoBehaviour
    {
        public void OnGameOver()
        {
            StartCoroutine(TriggerGameOver());
        }

        private IEnumerator TriggerGameOver()
        {
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene(SceneNames.LoseScene);
        }
    }
}

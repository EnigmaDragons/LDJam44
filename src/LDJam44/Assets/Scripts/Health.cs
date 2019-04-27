using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] Role role;
    [SerializeField] int maxHp;
    [SerializeField] int currentHp;
    [SerializeField] AudioClip onDeath;

    public void ApplyDamage(int amount)
    {
        currentHp -= amount;
        ResolveDestruction();
    }

    private void ResolveDestruction()
    {
        if (role.Equals(Role.Enemy) && currentHp <= 0)
        {
            AudioSource.PlayClipAtPoint(onDeath, transform.position);
        }

        if (role.Equals(Role.Player) && currentHp <= 0)
        {
            AudioSource.PlayClipAtPoint(onDeath, Camera.main.transform.position);
            StartCoroutine(LoadGameOverAfterDelay());
        }
    }

    private IEnumerator LoadGameOverAfterDelay()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneNames.LoseScene);
    }
}

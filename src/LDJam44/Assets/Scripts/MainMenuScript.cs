using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    public void PlayGame() {
        SceneManager.LoadScene("theGame");
    }
    public void QuitGame() {
        Application.Quit();
    }

}

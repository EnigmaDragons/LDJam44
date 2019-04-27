﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    public void NavigateToTitleScene() => SceneManager.LoadScene(SceneNames.TitleScene);
    public void NavigateToGameScene() => SceneManager.LoadScene(SceneNames.GameScene);
    public void NavigateToWinScene() => SceneManager.LoadScene(SceneNames.WinScene);
    public void NavigateToLoseScene() => SceneManager.LoadScene(SceneNames.LoseScene);
    public void QuitGame() => Application.Quit();
}
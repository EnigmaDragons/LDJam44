using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenUI : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SettingsUI;
    public AudioMixer Mixer;
    public Slider MusicVolume;
    public Slider SoundEffectsVolume;
    public AudioClip ButtonSound;

    private GameServices game;

    public void Start()
    {
        var gameState = GameObject.Find("GameState");
        if (gameState != null)
            gameState.GetComponent<GameState>().Reset();
        game = FindObjectOfType<GameServices>();
        float musicVolume = 0;
        Mixer.GetFloat("MusicVolume", out musicVolume);
        MusicVolume.value = musicVolume;
        float soundEffectVolume = 0;
        Mixer.GetFloat("SoundEffectVolume", out musicVolume);
        SoundEffectsVolume.value = soundEffectVolume;
    }

    public void Play()
    {
        game.PlaySoundEffect(ButtonSound);
        game.NavigateToScene(SceneNames.GameScene);
    }

    public void Settings()
    {
        game.PlaySoundEffect(ButtonSound);
        MainMenu.SetActive(false);
        SettingsUI.SetActive(true);
    }

    public void Credits()
    {
        game.PlaySoundEffect(ButtonSound);
        game.NavigateToScene(SceneNames.CreditsScene);
    }

    public void Exit()
    {
        game.PlaySoundEffect(ButtonSound);
        Application.Quit();
    }

    public void Return()
    {
        game.PlaySoundEffect(ButtonSound);
        SettingsUI.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void MusicVolumeChange(Slider slider)
    {
        Mixer.SetFloat("MusicVolume", slider.value);
    }

    public void SoundEffectVolumeChange(Slider slider)
    {
        Mixer.SetFloat("SoundEffectVolume", slider.value);
    }
}
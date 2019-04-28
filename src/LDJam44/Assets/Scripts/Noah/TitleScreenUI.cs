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

    public void Start()
    {
        float musicVolume = 0;
        Mixer.GetFloat("MusicVolume", out musicVolume);
        MusicVolume.value = musicVolume;
        float soundEffectVolume = 0;
        Mixer.GetFloat("SoundEffectVolume", out musicVolume);
        SoundEffectsVolume.value = soundEffectVolume;
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneNames.SpaceStationScene);
    }

    public void Settings()
    {
        MainMenu.SetActive(false);
        SettingsUI.SetActive(true);
    }

    public void Credits()
    {
        SceneManager.LoadScene(SceneNames.CreditsScene);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Return()
    {
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
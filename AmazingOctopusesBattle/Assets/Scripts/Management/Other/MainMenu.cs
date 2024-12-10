using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer sfxMixer = null;
    [SerializeField] private AudioMixer musicMixer = null;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("menuTheme");

        //Ajustes previos
        sfxMixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("sfxVolumeAudio", 1f)) * 20);
        musicMixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("musicVolumeAudio", 1f)) * 20);

        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualityConfig", 2));
        if (PlayerPrefs.GetInt("FSConfig", 0) == 1) Screen.fullScreen = true;
        if (PlayerPrefs.GetInt("FSConfig", 0) == 0) Screen.fullScreen = false;
    }

    public void PlayGame()
    {
        ClicSound();

        FindObjectOfType<AudioManager>().StopPlaying("menuTheme"); //pausar musica de menu
        Transitions transitions = FindObjectOfType<Transitions>();  //transition manager
        if (transitions != null) transitions.startTransition(1, 4);
    }

    public void tutorial()
    {
        ClicSound();

        FindObjectOfType<AudioManager>().StopPlaying("menuTheme"); //pausar musica de menu
        Transitions transitions = FindObjectOfType<Transitions>();  //transition manager
        if (transitions != null) transitions.startTransition(SceneManager.sceneCountInBuildSettings - 1, 4);
    }

    public void QuitGame()
    {
        ClicSound();
        Application.Quit();
    }

    public void ClicSound() => FindObjectOfType<AudioManager>().Play("Select");
}

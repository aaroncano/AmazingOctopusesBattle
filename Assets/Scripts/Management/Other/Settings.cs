using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer sfxMixer;
    public AudioMixer musicMixer;

    public Slider sfxAudioSlider;
    public Slider musicAudioSlider;

    public TMP_Dropdown QualityDropdown;
    public Toggle FullscreenToggle;

    Resolution[] resolutions;

    private void Start()
    {
        //Mostrar valores pasados de ajustes
        sfxAudioSlider.value = PlayerPrefs.GetFloat("sfxVolumeAudio", 1f);
        musicAudioSlider.value = PlayerPrefs.GetFloat("musicVolumeAudio", 1f);

        QualityDropdown.value = PlayerPrefs.GetInt("QualityConfig", 2);
        FullscreenToggle.isOn = FSisOnConverter();

        //Ajuste de resoluciones disponibles
        /*resolutions = Screen.resolutions;
        ResolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentRes = 0;
        for (int i=0; i<resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentRes = i;
            }
        }
        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = currentRes;
        ResolutionDropdown.RefreshShownValue();*/
    }

    public void sfxVolumeSlider(float volume)
    {
        PlayerPrefs.SetFloat("sfxVolumeAudio", volume);
        sfxMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }
    public void musicVolumeSlider(float volume)
    {
        PlayerPrefs.SetFloat("musicVolumeAudio", volume);
        musicMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void GraphicsSelection(int x)
    {
        PlayerPrefs.SetInt("QualityConfig", x);
        QualitySettings.SetQualityLevel(x);
    }

    public void FullscreenSet(bool x)
    {
        if (x == true) PlayerPrefs.SetInt("FSConfig", 1);
        if (x == false) PlayerPrefs.SetInt("FSConfig", 0);
        Screen.fullScreen = x;
    }

    /*public void ResolutionSelection(int x)
    {
        Resolution res = resolutions[x];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }*/

    public void Cliclsound()
    {
        FindObjectOfType<AudioManager>().Play("Select");
    }

    private bool FSisOnConverter()
    {
        if (PlayerPrefs.GetInt("FSConfig", 0) == 1) return true;
        else return false;
    }

}

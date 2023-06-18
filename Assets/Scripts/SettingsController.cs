using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Toggle vsync;
    public Toggle fullscreen;
    public Toggle postpoTog;

    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public GameObject postpo;

    public bool isMenu = false; 
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("vsync"))
            LoadOptions();

        // Para guardar los niveles de audio entre niveles
        float initLevel = 0f;
        mixer.GetFloat("MusicVolume", out initLevel);
        musicSlider.value = initLevel;

        mixer.GetFloat("SFXVolume", out initLevel);
        sfxSlider.value = initLevel;

        SaveOptions();
    }

    void LoadOptions()
    {
        int vsyncValue = PlayerPrefs.GetInt("vsync");
        if (vsyncValue == 0)
            vsync.isOn = false;
        else if (vsyncValue == 1)
            vsync.isOn = true;
        vsync.isOn = QualitySettings.vSyncCount != 0;

        int fullValue = PlayerPrefs.GetInt("fullValue");
        if (fullValue == 0)
            fullscreen.isOn = false;
        else if (fullValue == 1)
            fullscreen.isOn = true;
        fullscreen.isOn = Screen.fullScreen;

        int pospoValue = PlayerPrefs.GetInt("pospoValue");
        if (pospoValue == 0)
            postpoTog.isOn = false;
        else if (pospoValue == 1)
            postpoTog.isOn = true;
        if (!isMenu)
        {
            postpo.SetActive(postpoTog.isOn);
        }
    }

    void SaveOptions()
    {
        int vsyncValue = 0;
        if (vsync.isOn)
            vsyncValue = 1;
        else
            vsyncValue = 0;
        PlayerPrefs.SetInt("vsync", vsyncValue);

        int fullValue = 0;
        if (fullscreen.isOn)
            fullValue = 1;
        else
            fullValue = 0;
        PlayerPrefs.SetInt("fullValue", fullValue);

        int pospoValue = 0;
        if (postpoTog.isOn)
            pospoValue = 1;
        else
            pospoValue = 0;
        PlayerPrefs.SetInt("pospoValue", pospoValue);
    }

    public void HandleSFX(float level)
    {
        mixer.SetFloat("SFXVolume", sfxSlider.value);

        // Cuando el valor este al minimo resta - 40 para asegurarse de que la musica no se escuche
        if (sfxSlider.value == sfxSlider.minValue)
            mixer.SetFloat("SFXVolume", -80);
    }
    public void HandleMusic(float level)
    {
        mixer.SetFloat("MusicVolume", musicSlider.value);

        // Cuando el valor este al minimo resta - 40 para asegurarse de que la musica no se escuche
        if (musicSlider.value == musicSlider.minValue)
            mixer.SetFloat("MusicVolume", -80);
    }

    public void HandleVsync(bool value)
    {
        QualitySettings.vSyncCount = value ? 1 : 0;
        SaveOptions();
    }

    public void HandleFullscreen(bool value)
    {
        Debug.Log(value);
        Screen.fullScreen = value;
        SaveOptions();
    }

    public void HandlePostPo(bool value)
    {
        postpo.SetActive(value);
        Debug.Log(value);
        SaveOptions();
    }
}
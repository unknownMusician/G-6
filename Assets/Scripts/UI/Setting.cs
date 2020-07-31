using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    private bool isFullScreen;
    public AudioMixer audiomixer;
    public static bool prap = false;
    public GameObject mainMenu;
    public GameObject setting;

    public void FullScreenToggle()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }


    public void Awake()
    {
        isFullScreen = Screen.fullScreen;
    }

    public void Exit()
    {
        setting.SetActive(false);
        if (prap)
        {
            mainMenu.SetActive(true);
            prap = false;
        }
        Pause.GameIsPaused = false;
    }

    public void AudioVolume(float sliderValue)
    {
        audiomixer.SetFloat("masterVolume", sliderValue);
    }
    public void Quality(int q)
    {
        QualitySettings.SetQualityLevel(q);
    }
}

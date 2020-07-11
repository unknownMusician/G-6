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
    private Resolution[] rsl;
    private List<string> resolutions;
    public AudioMixer audiomixer;
    public Dropdown resolutiondDropdown;
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
        resolutions = new List<string>();
        rsl = Screen.resolutions;
        foreach (var i in rsl)
        {
            resolutions.Add(i.width + "x" + i.height);
        }
        resolutiondDropdown.ClearOptions();
        resolutiondDropdown.AddOptions(resolutions);
    }

    public void Exit()
    {
        setting.SetActive(false);
        if (prap)
        {
            mainMenu.SetActive(true);
            prap = false;
        }
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
    }

    public void Resolution(int r)
    {
        Screen.SetResolution(rsl[r].width, rsl[r].height, isFullScreen);
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

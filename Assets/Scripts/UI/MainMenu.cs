using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject settingGameObject;
    public GameObject thisGameObject;
    public AudioMixer audioGameObject;
    public GameObject gameUIGameObject;
    public GameObject weaponSettingsGameObject;
    public void PlayPressed()
    {
        DontDestroyOnLoad(settingGameObject);
        DontDestroyOnLoad(audioGameObject);
        DontDestroyOnLoad(gameUIGameObject);
        DontDestroyOnLoad(weaponSettingsGameObject);

        gameUIGameObject.SetActive(true);

        SceneManager.LoadScene("Level 1");
    }

    public void LoadSetting()
    {
        thisGameObject.SetActive(false);
        settingGameObject.SetActive(true);
        Setting.prap = true;
    }


    void Start()
    {
        audioGameObject.SetFloat("masterVolume", -25);
    }
    public void ExitPressed()
    {
        Application.Quit();
    }
    //public void LoadSetting()
    //{
    //    settinGameObject.active = true;
    //}

    public void LoadGame()
    {

    }
}

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
    public void PlayPressed()
    {
        DontDestroyOnLoad(settingGameObject);
        DontDestroyOnLoad(audioGameObject);
        DontDestroyOnLoad(gameUIGameObject);

        gameUIGameObject.active = true;

        SceneManager.LoadScene("Level 1");
    }

    public void LoadSetting()
    {
        thisGameObject.active = false;
        settingGameObject.active = true;
        Setting.prap = true;
    }


    void Start()
    {
        audioGameObject.SetFloat("masterVolume", -60);
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

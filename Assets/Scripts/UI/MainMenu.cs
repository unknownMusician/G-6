using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingGameObject;
    public GameObject audioGameObject;
    public GameObject gameUIGameObject;
    public void PlayPressed()
    {
        DontDestroyOnLoad(settingGameObject);
        DontDestroyOnLoad(audioGameObject);
        DontDestroyOnLoad(gameUIGameObject);
        gameUIGameObject.active = true;

        SceneManager.LoadScene("Level 1");
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

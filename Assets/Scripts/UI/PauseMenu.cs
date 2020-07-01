using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    void Update()
    {
        if (Input.GetKeyDown((KeyCode.Escape)))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
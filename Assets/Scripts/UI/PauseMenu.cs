using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    
    public static void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public static void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
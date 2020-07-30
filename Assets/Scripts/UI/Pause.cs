using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private static bool gameIsPaused = false;
    public static bool GameIsPaused {
        get => gameIsPaused;
        set {
            gameIsPaused = value;
            Time.timeScale = GameIsPaused ? 0f : 1f;
        }
    }
}
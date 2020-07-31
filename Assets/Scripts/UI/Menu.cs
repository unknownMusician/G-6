using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject destroy1;
    public GameObject destroy2;
    public GameObject destroy3;
    public GameObject destroy4;
    public void LoadMainMenu()
    {
        Destroy(destroy1);
        Destroy(destroy2);
        Destroy(destroy3);
        Destroy(destroy4);
        SceneManager.LoadScene("MainMenu");
    }
    public void Continue()
    {
        this.gameObject.SetActive(false);
        Pause.GameIsPaused = false;
    }
    public void LoadSetting()
    {
    }
}

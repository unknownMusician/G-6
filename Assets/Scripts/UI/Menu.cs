using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Continue()
    {
        this.gameObject.active = false;
    }
    public void LoadSetting()
    {
    }
}

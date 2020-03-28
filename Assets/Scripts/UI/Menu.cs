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

        //SceneManager.LoadScene("SampleScene");
    }
    public void LoadSetting()
    {
        SceneManager.LoadScene("Setting");
    }
}

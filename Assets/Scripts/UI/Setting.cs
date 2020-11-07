using UnityEngine;
using UnityEngine.Audio;

namespace G6.UI {
    public class Setting : MonoBehaviour
    {
        public static Setting instance;

        private bool isFullScreen;
        public AudioMixer audiomixer;
        public static bool prap = false;

        public void FullScreenToggle()
        {
            isFullScreen = !isFullScreen;
            Screen.fullScreen = isFullScreen;
        }


        public void Awake()
        {
            instance = this;
            isFullScreen = Screen.fullScreen;
            gameObject.SetActive(false);
        }
        public void OnDestroy() => instance = null;

        public void Exit()
        {
            gameObject.SetActive(false);
            if (prap)
            {
                MainMenu.instance.gameObject.SetActive(true);
                prap = false;
            }
            Pause.GameIsPaused = false;
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
}
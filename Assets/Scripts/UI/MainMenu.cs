using G6.Data;
using G6.Data.SaveLoad;
using UnityEngine;
using UnityEngine.Audio;

namespace G6.UI {
    public class MainMenu : MonoBehaviour
    {
        public static MainMenu instance;
        protected void Awake() => instance = this;
        protected void OnDestroy() => instance = null;

        public AudioMixer audioGameObject;

        public void PlayPressed() => LevelManager.LoadLevel(LevelManager.Level.Level1);
        public void LoadPressed() => SaveAndLoadGame.LoadGame();
        public void BuilderPressed() => LevelManager.LoadEnvironmentBuilder();
        public void ExitPressed() => Application.Quit();

        public void LoadSetting() {
            gameObject.SetActive(false);
            Setting.instance.gameObject.SetActive(true);
            Setting.prap = true;
        }


        void Start()
        {
            audioGameObject.SetFloat("masterVolume", -25); // todo Make AudioManager
        }
    }
}
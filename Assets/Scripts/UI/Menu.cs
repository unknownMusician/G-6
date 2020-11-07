using G6.Data;
using UnityEngine;

namespace G6.UI {
    public class Menu : MonoBehaviour
    {
        public static Menu instance;

        protected void Awake() {
            instance = this;
            gameObject.SetActive(false);
        }
        protected void OnDestroy() => instance = null;

        public void LoadMainMenu()
        {
            LevelManager.LoadMenu();
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
}
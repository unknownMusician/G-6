using UnityEngine;

namespace G6.UI {
    public static class Pause
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
}
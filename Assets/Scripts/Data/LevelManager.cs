using UnityEngine.SceneManagement;

namespace G6.Data {
    public static class LevelManager {
        public static void LoadLevel(Level level) {
            SceneManager.LoadScene(GetLevelName(level));
            // todo
        }
        public static void LoadHub() {
            SceneManager.LoadScene("Level Hub");
            // todo
        }
        public static void LoadEnvironmentBuilder() {
            SceneManager.LoadScene("EnvironmentBuilder");
            // todo
        }
        public static void LoadPlayTest() {
            SceneManager.LoadScene("PlayTest");
            // todo
        }
        public static void LoadMenu() {
            SceneManager.LoadScene("MainMenu");
            // todo
        }

        private static string GetLevelName(Level level) {
            switch (level) {
                case Level.Level1:
                    return "Level 1";
                default:
                    return null;
            }
        }

        public enum Level {
            Level1
        }
    }
}